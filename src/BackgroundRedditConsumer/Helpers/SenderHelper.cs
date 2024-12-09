using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http.Headers;

namespace BackgroundRedditConsumer.Helpers
{
    public static class SenderHelper
    {
        public static async Task<HttpResponseHeaders> SendCommand(ISender sender, Type commandType, 
            string subRedditConfig, string timeframeType, 
            ushort limit, CancellationToken stoppingToken)
        {
            var commandInstance = Activator.CreateInstance(commandType, subRedditConfig, timeframeType, limit);
            var methodInfo = sender.GetType().GetMethod("Send", [typeof(IRequest), typeof(CancellationToken)]);
            var task = (Task)(methodInfo?.Invoke(sender, [commandInstance!, stoppingToken]))!;
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var taskResult = resultProperty?.GetValue(task);
            HttpResponseHeaders httpResultHeaders = (HttpResponseHeaders)taskResult?.GetType()?.GetField("Item2")?.GetValue(taskResult)!;

            return httpResultHeaders;
        }
    }
}
