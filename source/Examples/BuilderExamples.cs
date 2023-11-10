using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HttpClientBuilder.Requests;

namespace HttpClientBuilder.Examples
{
    internal class BuilderExamples
    {
        public async Task<bool> ExampleGetWeatherAndCheckIfItsNice()
        {
           var client = ClientBuilder.CreateBuilder()
                .WithHost("172.26.6.104")
                .WithBaseRoute("api/weather")
                .WithBearerToken("JWT TOKEN HERE")
                .WithSelfSignedCerts()
                .WithHeader("x-api-key", "this is an extra header")
                .BuildClient();

            var response = await client
                .GetContentFromJsonAsync<Weather>("forecast")
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

            var getWeatherRequest = client.AddGetHandler("forecast", new WeatherForecastHandler());
            var getSnowRequest = client.AddGetHandler("snow", new WeatherForecastHandler());
            var getRainRequest = client.AddGetHandler("rain", new WeatherForecastHandler());

            await Task.WhenAll(
                getRainRequest.DispatchAsync(), 
                getSnowRequest.DispatchAsync(), 
                getWeatherRequest.DispatchAsync());

            return response.Success;
        }

        public async Task CreateHandlersAndDispatchRequests()
        {
            // Create the client using the builder

            var client = ClientBuilder.CreateBuilder()
                .WithHost("172.26.6.104")
                .WithBaseRoute("api/weather")
                .BuildClient();

            // Create IDispatchHandlers by calling the Add[Verb]Handler method
            // Provide a path and passing an IRequestHandler
            // Your handles could certainly be coming from a DI container

            var getWeatherRequest = client.AddGetHandler("forecast", new WeatherForecastHandler());
            var getSnowRequest = client.AddPostHandler("snow", new WeatherForecastHandler());
            var getRainRequest = client.AddGetHandler("rain", new WeatherForecastHandler());

            // Await the DispatchAsync method on the handle.
            // You can optionally pass HTTP content into the function to send as the body of your request.

            await Task.WhenAll(
                getRainRequest.DispatchAsync(),
                getSnowRequest.DispatchAsync(new StringContent("HELLO Content")),
                getWeatherRequest.DispatchAsync());

            // You can also create a life cycle request.
            using var getUglyDWeather = client.AddGetHandler("ugly", new WeatherForecastHandler());
            await getUglyDWeather.DispatchAsync();

            // Your IDispatch instance will now be disposed
        }
    }

    internal class WeatherForecastHandler : IRequestHandler
    {

        #region Implementation of IRequestHandler

        /// <inheritdoc />
        public async Task HandleRequest(HttpStatusCode code, HttpResponseHeaders headers, HttpContent content)
        {
            // Called when no post request processing is required.
        }

        /// <inheritdoc />
        public async Task HandleRequest<TValue>(HttpStatusCode code, HttpResponseHeaders headers, TValue body)
        {
            // Called when post request body processing is required.
        }

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            // The handles dispose method will be invoked if you dispose of the IDispatch Request.
        }

        #endregion
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
