
using Application.Commands;
using Application.Models;
using BackgroundRedditConsumer.Helpers;
using Domain.Abstractions;
using Domain.Extensions;
using Domain.Models.Config;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace BackgroundRedditConsumer
{
    public class RedditHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _factory;
        private readonly RedditConfiguration _redditConfiguration;
        private readonly ILogger<RedditHostedService> _logger;
        private long _executionCount = 0;
        private long _lastExecutionCount = 0;
        private long _executionCountPerSecond = 0;

        private readonly int _maxQueriesPerMinute;
        private readonly decimal _maxNumReqsPerSecond;

        /// <summary>
        /// Given that max queries per minute is 100
        /// If a requests takes less than 600 ms, then the remaining time should be throttled
        /// </summary>
        private readonly decimal _throttledMillisecondsForRequest;

        private const string xRateLimitUsedHeaderName = "x-ratelimit-used";
        private const string xRateLimitRemainingHeaderName = "x-ratelimit-remaining";
        private const string xRateLimitResetHeaderName = "x-ratelimit-reset";

        private readonly TimeSpan _periodicTimerTimeSpan = TimeSpan.FromSeconds(1);
        private TimeSpan _semaphoreTimeSpan = TimeSpan.FromMilliseconds(-1);
        private bool _hasReachedLimit = false;

        public bool IsEnabled { get; set; } = true;

        public RedditHostedService(IServiceScopeFactory factory,
            IOptionsMonitor<RedditConfiguration> redditConfiguration,
            ILogger<RedditHostedService> logger)
        { 
            _factory = factory.ThrowIfNull(nameof(factory));
            _redditConfiguration = redditConfiguration.CurrentValue.ThrowIfNull(nameof(redditConfiguration));
            _logger = logger.ThrowIfNull(nameof(logger));

            _maxQueriesPerMinute = _redditConfiguration.MaxQueriesPerMinute;
            _maxNumReqsPerSecond = _maxQueriesPerMinute / 60M;

            //[1000 ms / (100 / 60) = 600 ms]
            _throttledMillisecondsForRequest = 1000 / _maxNumReqsPerSecond;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timeframeType = _redditConfiguration.TimeFrameType;
            var limit = _redditConfiguration.Limit;

            ushort rateLimitUsed;
            decimal rateLimitRemaining;
            ushort rateLimitResetSeconds = 0;

            List<Type> redditStatisticsCommands = [typeof(AddPostsWithMostVotesCommand), typeof(AddUsersWithMostPostsCommand)];

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (IsEnabled && _redditConfiguration.SubRedditList?.Count > 0)
                    {
                        var numOfThreads = redditStatisticsCommands.Count * _redditConfiguration.SubRedditList.Count;
                        using var semaphoreSlim = new SemaphoreSlim(numOfThreads, 100);

                        var tasks = _redditConfiguration.SubRedditList.SelectMany(sub => redditStatisticsCommands,
                                     async (subRedditConfig, commandType) =>
                        {
                            var delayTimeSpan = _semaphoreTimeSpan.CompareTo(TimeSpan.FromMilliseconds(-1)) == 0 ? TimeSpan.FromMilliseconds(0) : _semaphoreTimeSpan;

                            _logger.LogDebug("Delay before wait async: {DelayTimeSpan}", delayTimeSpan.TotalMilliseconds);
                            //await Task.Delay(delayTimeSpan / numOfThreads);
                            await Task.Delay(delayTimeSpan);

                            if (_hasReachedLimit)
                            {
                                _hasReachedLimit = false;
                            }

                            await semaphoreSlim.WaitAsync(TimeSpan.FromMilliseconds(-1), stoppingToken);

                            try
                            {
                                await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                                var sender = asyncScope.ServiceProvider.GetRequiredService<ISender>();

                                HttpResponseHeaders httpResultHeaders = await SenderHelper.SendCommand(sender, commandType, subRedditConfig, timeframeType, limit, stoppingToken);

                                _executionCount++;
                                _executionCountPerSecond++;
                                _logger.LogInformation("Executed RedditService for {CommandType} {SubRedditName} - Count: {Count}", commandType, subRedditConfig, _executionCount);

                                lock (this)
                                {
                                    if (httpResultHeaders.TryGetValues(xRateLimitUsedHeaderName, out var rateLimitUsedValues) &&
                                        httpResultHeaders.TryGetValues(xRateLimitRemainingHeaderName, out var rateLimitRemainingValues) &&
                                        httpResultHeaders.TryGetValues(xRateLimitResetHeaderName, out var rateLimitResetValues)
                                    )
                                    {
                                        lock (this)
                                        {
                                            ushort.TryParse(rateLimitUsedValues.FirstOrDefault(), out rateLimitUsed);
                                            decimal.TryParse(rateLimitRemainingValues.FirstOrDefault(), out rateLimitRemaining);
                                            ushort.TryParse(rateLimitResetValues.FirstOrDefault(), out rateLimitResetSeconds);

                                            _logger.LogInformation("Rate Limit Remaining for subreddit {CommandType} {Subreddit}: {RateLimitRemaining} with Limit Reset of {RateLimitReset}",
                                                commandType, subRedditConfig, rateLimitRemaining, rateLimitResetSeconds);

                                            //If remaining requests is = 0
                                            //Then throttle for the remaining rate limit reset
                                            if (rateLimitRemaining == 0)
                                            {
                                                _logger.LogDebug("Number of requests has reached its limit. Rate Limit Reset is: {RateLimitReset}", rateLimitResetSeconds);
                                                lock (this)
                                                {
                                                    _hasReachedLimit = true;
                                                    _semaphoreTimeSpan = TimeSpan.FromMilliseconds(rateLimitResetSeconds * 1000);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                semaphoreSlim.Release();
                            }
                        }).ToList();

                        //Do this only the first time to allow execution of at least one request to determine the limit reset
                        if (_executionCount < 2)
                        {
                            await Task.Delay(1000, stoppingToken);
                        }

                        //Add timer throttler asynchronously. Skip if limit has been reached
                        if (!_hasReachedLimit)
                        {
                            //await Task.Run(async () =>
                            //{
                            //    await PeriodicTimerThrottler(rateLimitResetSeconds, stoppingToken);
                            //}, stoppingToken);
                            tasks.Add(PeriodicTimerThrottler(rateLimitResetSeconds, stoppingToken));
                        }

                        // Wait for all tasks to complete
                        await Task.WhenAll(tasks);
                    }
                    else
                    {
                        _logger.LogInformation("Skipped RedditService");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to execute RedditService with exception message {@Exception}", ex.Message);
                }
            }
        }

        public async Task PeriodicTimerThrottler(ushort rateLimitResetSeconds, CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(_periodicTimerTimeSpan);

            //Run a 1-second timer to weight the number of requests executed and throttle accordingly
            await timer.WaitForNextTickAsync(stoppingToken);

            _logger.LogDebug("Periodic Timer - Entering...");

            lock (this)
            {
                _lastExecutionCount = _executionCount;

                _logger.LogDebug("Periodic Timer - Requests per second {@RequestsPerSecond}", _executionCountPerSecond);

                //For a QPM value of 100, this variable's value is (NumReqs * 600 ms)
                var millisecondsPerNRequests = _executionCountPerSecond * _throttledMillisecondsForRequest;
                var exceededMilliseconds = millisecondsPerNRequests - 1150;

                if (exceededMilliseconds > 0)
                {
                    _logger.LogDebug("Periodic Timer - Exceeded Milliseconds: {@ExceededMilliseconds}", exceededMilliseconds);
                    _logger.LogDebug("Periodic Timer - Rate Limit Reset Milliseconds: {@RateLimitResetMilliseconds}", (1000 * rateLimitResetSeconds));

                    //Skip only if limit has not been reached
                    if (!_hasReachedLimit)
                    {
                        if (exceededMilliseconds > (1000 * rateLimitResetSeconds))
                        {
                            _semaphoreTimeSpan = TimeSpan.FromMilliseconds(1000 * rateLimitResetSeconds);
                        }
                        else
                        {
                            _semaphoreTimeSpan = TimeSpan.FromMilliseconds((double)exceededMilliseconds);
                        }
                    }
                }

                //Reset counter
                _executionCountPerSecond = 0;
            }
        }
    }
}