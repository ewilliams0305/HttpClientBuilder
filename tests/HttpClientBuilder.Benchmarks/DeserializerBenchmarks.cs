using BenchmarkDotNet.Attributes;
using HttpClientBuilder.Benchmarks.Model;
using HttpClientBuilder.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using WeatherForecastContext = HttpClientBuilder.Benchmarks.Model.WeatherForecastContext;
using WeatherListForecastContext = HttpClientBuilder.Benchmarks.Model.WeatherListForecastContext;

#pragma warning disable CA1822

namespace HttpClientBuilder.Benchmarks
{
    [MemoryDiagnoser]
    public class DeserializerBenchmarks
    {
        private const string JsonString = @"{
  ""Date"": ""2019-08-01T00:00:00"",
  ""TemperatureC"": 25,
  ""Summary"": ""Hot""
}
";

        private static IHttpClient? Client;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var factory = new WebApplicationFactory<IAssemblyMarker>()
                .WithWebHostBuilder(configuration =>
                {
                    
                });

            Client = ClientBuilder.CreateBuilder()
                .WithHost("127.0.0.1")
                .BuildClient(() => factory.CreateClient());

        }

        [Benchmark]
        public async Task GetObject_SourceGenerated_Deserializer()
        {
            await Client?
                .GetContentFromJsonAsync<List<WeatherForecast>>("weatherForecast", WeatherListForecastContext.Default)!;
        }
        [Benchmark]
        public async Task GetObject_WithoutSourceGenerated_Deserializer()
        {
            await Client?
                .GetContentFromJsonAsync<List<WeatherForecastWithOutContext>>("weatherForecast")!;
        }

        [Benchmark]
        public void Deserialize_SourceGenerated_Deserializer()
        {
            JsonSerializer.Deserialize(JsonString, WeatherForecastContext.Default.WeatherForecast);
        }
        [Benchmark]
        public void Deserialize_WithoutSourceGenerated_Deserializer()
        {
            JsonSerializer.Deserialize<WeatherForecastWithOutContext>(JsonString);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Client?.Dispose();
        }
    }
}