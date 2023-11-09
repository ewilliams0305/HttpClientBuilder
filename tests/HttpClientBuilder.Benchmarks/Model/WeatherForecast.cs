
#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace HttpClientBuilder.Benchmarks.Model;

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public WeatherForecast()
    {

    }
}

public class WeatherForecastWithOutContext
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public WeatherForecastWithOutContext()
    {

    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(WeatherForecast), GenerationMode = JsonSourceGenerationMode.Default)]
internal partial class WeatherForecastContext : JsonSerializerContext
{
}


[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(List<WeatherForecast>), GenerationMode = JsonSourceGenerationMode.Default)]
internal partial class WeatherListForecastContext : JsonSerializerContext
{
}