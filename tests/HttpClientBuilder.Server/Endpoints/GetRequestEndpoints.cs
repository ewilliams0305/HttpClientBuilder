using HttpClientBuilder.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace HttpClientBuilder.Server.Endpoints;

public static class GetRequestEndpoints
{
    public static WebApplication MapGetEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => new WeatherForecast
        (
            DateTime.Now.AddDays(1),
            Random.Shared.Next(-20, 55),
            Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
        ));

        app.MapGet("/api", () => new WeatherForecast
        (
            DateTime.Now.AddDays(1),
            Random.Shared.Next(-20, 55),
            Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
        ));

        app.MapGet("/api/weather/error", ([FromQuery] bool error) =>
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

        app.MapGet("/api/weather/exception", ([FromQuery] bool error) =>
        {
            throw new Exception();
        });

        app.MapGet("/api/weatherforecast", () =>
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