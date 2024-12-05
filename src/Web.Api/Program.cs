using Domain.Models.Config;
using Infrastructure.Extensions;
using Serilog;
using MediatR;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Application.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Scrutor;
using Application.Models;
using Domain.Abstractions;

var builder = WebApplication.CreateBuilder(args);

//Mediatr for CQRS and Clean Architecture
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

//Scrutor to resolve services
builder.Services.Scan(selector => selector
    .FromAssemblies(
        Application.AssemblyReference.Assembly,
        Infrastructure.AssemblyReference.Assembly)
    .AddClasses(publicOnly: false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithScopedLifetime());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Configuration
    .AddJsonFile(Path.Combine(builder.Environment.ContentRootPath, "appsettings.json"), optional: false, reloadOnChange: false)
    .AddJsonFile(Path.Combine(builder.Environment.ContentRootPath, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: false, reloadOnChange: false)
    .AddUserSecrets(Assembly.GetExecutingAssembly())
.AddEnvironmentVariables();

builder.Services.Configure<RedditConfiguration>(builder.Configuration);

var redditClientId = builder.Configuration["RedditConfiguration:RedditAuth:clientId"] ?? string.Empty;
var redditSecret = builder.Configuration["RedditConfiguration:RedditAuth:secret"] ?? string.Empty;
var redditTokenBaseUrl = builder.Configuration["RedditConfiguration:RedditTokenBaseUrl"] ?? string.Empty;
var redditApiBaseUrl = builder.Configuration["RedditConfiguration:RedditApiBaseUrl"] ?? string.Empty;
var redditAuthToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{redditClientId}:{redditSecret}"));

builder.Services.AddReditServices(redditApiBaseUrl, redditTokenBaseUrl, authorizationToken: redditAuthToken);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapPost("/postswithmostvotes", async Task<Results<Created<IEnumerable<InsertSubRedditPosts>>, ProblemHttpResult>> (ISender sender, AddPostsWithMostVotesCommand command) =>
{
    (var result, var httpResultHeaders) = await sender.Send(command);

    if (result.IsSuccess)
    {
        return TypedResults.Created("", result.Value);
    }

    return TypedResults.Problem(result.Error.Message, title: result.Error.Code, statusCode: result.Error.StatusCode);
})
.WithName("PostsWithMostVotes")
.WithOpenApi();

app.Run();