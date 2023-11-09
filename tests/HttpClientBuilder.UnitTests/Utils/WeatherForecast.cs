using System.Text.Json.Serialization;

#pragma warning disable CS8618

namespace HttpClientBuilder.UnitTests.Utils;

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

[JsonSerializable(typeof(WeatherForecast), GenerationMode = JsonSourceGenerationMode.Default)]
internal partial class WeatherForecastContext : JsonSerializerContext
{
}

[JsonSerializable(typeof(IEnumerable<WeatherForecast>), GenerationMode = JsonSourceGenerationMode.Default)]
internal partial class WeatherListForecastContext : JsonSerializerContext
{
}
