using System.Net;
using System.Net.Http.Headers;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

public class TestingGenericHandler : IRequestHandler<WeatherForecast>
{
    public bool SuccessBody;
    public bool Failed;

    #region Implementation of IRequestHandler<in WeatherForecast,in ErrorResponse>

    /// <inheritdoc />
    public Task HandleBody(HttpStatusCode code, HttpResponseHeaders headers, WeatherForecast content)
    {
        SuccessBody = code is >= (HttpStatusCode)200 and < (HttpStatusCode)400 && content != null;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task HandleException(Exception exception)
    {
        Failed = exception != null;
        return Task.CompletedTask;
    }

    #endregion
}