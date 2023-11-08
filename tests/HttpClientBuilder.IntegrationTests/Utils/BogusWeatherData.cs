using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace HttpClientBuilder.IntegrationTests.Utils
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
