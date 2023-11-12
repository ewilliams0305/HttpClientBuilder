using HttpClientBuilder.Server.Models;

namespace HttpClientBuilder.Server.Endpoints;

public static class GetRequestEndpoints
{
    public static WebApplication MapGetEndpoints(this WebApplication app)
    {

        app.MapGet("/api", () => new WeatherForecast
        (
            DateTime.Now.AddDays(1),
            Random.Shared.Next(-20, 55),
            Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
        ));

        app.MapGet("/", () => new WeatherForecast
        (
            DateTime.Now.AddDays(1),
            Random.Shared.Next(-20, 55),
            Summaries.Values[Random.Shared.Next(Summaries.Values.Length)]
        ));

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