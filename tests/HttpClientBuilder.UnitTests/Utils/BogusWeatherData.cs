
using Bogus;

namespace HttpClientBuilder.UnitTests.Utils
{
    public static class BogusWeatherData
    {

        public static WeatherForecast GetForecast()
        {
            var faker = new Faker<WeatherForecast>()
                .RuleFor(w => w.Date, f => f.Date.Soon())
                .RuleFor(w => w.Summary, f => f.Locale)
                .RuleFor(w => w.TemperatureC, 30)
                .RuleFor(w => w.TemperatureF, 75);

            return faker.Generate();
        }
    }
}
