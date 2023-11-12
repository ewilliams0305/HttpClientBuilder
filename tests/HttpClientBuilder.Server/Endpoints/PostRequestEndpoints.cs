using HttpClientBuilder.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientBuilder.Server.Endpoints;

public static class PostRequestEndpoints
{
    public static WebApplication MapPostEndpoints(this WebApplication app)
    {
        app.MapPost("/", ([FromBody]WeatherForecast? forecast) =>
            
            forecast ?? new WeatherForecast
            (
                DateTime.Now.AddDays(1),
                Random.Shared.Next(-20, 55),
                Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
            ));

        app.MapPost("/api", ([FromBody] WeatherForecast? forecast) =>
        {
            var result = forecast ?? new WeatherForecast
            (
                DateTime.Now.AddDays(1),
                Random.Shared.Next(-20, 55),
                Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
            );

            return Results.Ok(result);
        });
        
        app.MapPost("/api/echo", ([FromBody] Echo echo) =>
        {
            return Results.Ok(echo);
        });
            

        app.MapPost("/api/weather/error", ([FromQuery] bool error) =>
        {
            return error
                ? Results.BadRequest(new ErrorResponse(DateTime.Now, "Request was  a failure", 400,
                    new[] { "this weather sucks, You told me to error" }))
                : Results.Ok(new WeatherForecast
                (
                    DateTime.Now.AddDays(1),
                    Random.Shared.Next(-20, 55),
                    Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
                ));
        });

        app.MapPost("/api/weather/exception", ([FromQuery] bool error) =>
        {
            throw new Exception();
        });

        app.MapPost("/api/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateTime.Now.AddDays(index),
                        Random.Shared.Next(-20, 55),
                        Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
                    ))
                .ToArray();
            return forecast;
        });

        return app;
    }
}