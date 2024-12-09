using Application.Behaviors;
using Application.Commands;
using Application.Models;
using Application.Queries;
using BackgroundRedditConsumer;
using BackgroundRedditConsumer.Infrastructure;
using Domain.Models.Config;
using FluentValidation;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Scrutor;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Mediatr for CQRS and Clean Architecture and Mediatr's behavior fluent validation
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly,
        AssemblyReference.Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

//Scrutor to resolve services
builder.Services.Scan(selector => selector
    .FromAssemblies(
        Application.AssemblyReference.Assembly,
        Infrastructure.AssemblyReference.Assembly
        )
    .AddClasses(publicOnly: false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithScopedLifetime());

//Add fluent validation
builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Configuration
    .AddJsonFile(Path.Combine(builder.Environment.ContentRootPath, "appsettings.json"), optional: false, reloadOnChange: false)
    .AddJsonFile(Path.Combine(builder.Environment.ContentRootPath, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: false, reloadOnChange: false)
    .AddUserSecrets(Assembly.GetExecutingAssembly())
.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RedditHostedService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<RedditHostedService>());

builder.Services.Configure<RedditConfiguration>(builder.Configuration.GetSection(RedditConfiguration.Section));

var redditClientId = builder.Configuration["RedditConfiguration:RedditAuth:clientId"] ?? string.Empty;
var redditSecret = builder.Configuration["RedditConfiguration:RedditAuth:secret"] ?? string.Empty;
var redditTokenBaseUrl = builder.Configuration["RedditConfiguration:RedditTokenBaseUrl"] ?? string.Empty;
var redditApiBaseUrl = builder.Configuration["RedditConfiguration:RedditApiBaseUrl"] ?? string.Empty;
var redditAuthToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{redditClientId}:{redditSecret}"));

builder.Services.AddReditServices(redditApiBaseUrl, redditTokenBaseUrl, authorizationToken: redditAuthToken);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapPost("/realtime/postswithmostvotes", async Task<Results<Ok<List<InsertSubRedditPosts>>, NotFound>> (ISender sender, [FromBody] GetRealTimePostsWithMostVotesQuery query) =>
{
    var lastSubRedditPosts = await sender.Send(query);

    if (lastSubRedditPosts != null && lastSubRedditPosts.Count > 0)
    {
        return TypedResults.Ok(lastSubRedditPosts);
    }

    return TypedResults.NotFound();
})
.WithName("GetRealTimePostsWithMostVotes")
.WithOpenApi();

app.MapPost("/realtime/userswithmostposts", async Task<Results<Ok<List<AuthorPostsCountDetails>>, NotFound>> (ISender sender, [FromBody] GetRealTimeUsersWithMostPostsQuery query) =>
{
    var lastSubRedditPosts = await sender.Send(query);

    if (lastSubRedditPosts != null && lastSubRedditPosts.Count > 0)
    {
        return TypedResults.Ok(lastSubRedditPosts);
    }

    return TypedResults.NotFound();
})
.WithName("GetRealTimeUsersWithMostPosts")
.WithOpenApi();

app.Run();