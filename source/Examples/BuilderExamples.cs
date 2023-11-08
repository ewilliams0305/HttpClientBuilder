using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HttpClientBuilder.Model;

namespace HttpClientBuilder.Examples
{
    internal class BuilderExamples
    {
        public async Task<bool> ExampleGetWeatherAndCheckIfItsNice()
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("172.26.6.104")
                .ConfigureBearerToken("JWT TOKEN HERE")
                .AcceptSelfSignedCerts()
                .WithHeader("x-api-key", "this is an extra header")
                .CreateClient();

            var response = await client
                .GetContentAsync<Weather>("weather")
                .EnsureAsync(
                    predicate: (weather) => weather.IsNice && weather.Temperature > 60,
                    errorFactory: () => new Exception("THE WEATHER IS NOT NICE"))
                .HandleAsync(
                    value: (code, weather) =>
                    {
                        //PROCESS WEATHER ONLY IF THE WEATHER IS NICE AND GREATER THAN 60
                    },
                    error: (exception) =>
                    {
                        //PROCESS ERROR ONLY IF THE REQUEST FAILED OR THE WEATHER IS NOT NICE
                    });

            return response.Success;
        }
    }

    internal class Weather
    {
        public Weather()
        {

        }

        public bool IsNice { get; set; }
        public int Temperature { get; set; }
    }
}
