<img src="./docs/http_logo.png" alt="drawing" width="100"/>

# HTTP Client Builder
![GitHub](https://img.shields.io/github/license/ewilliams0305/ClientBuilder) 
![GitHub all releases](https://img.shields.io/github/downloads/ewilliams0305/ClientBuilder/total) 
![Nuget](https://img.shields.io/nuget/dt/ClientBuilder)
![GitHub issues](https://img.shields.io/github/issues/ewilliams0305/ClientBuilder)
![GitHub Repo stars](https://img.shields.io/github/stars/ewilliams0305/ClientBuilder?style=social)
![GitHub forks](https://img.shields.io/github/forks/ewilliams0305/ClientBuilder?style=social)

Client builder is a configuration builder for HTTP request piplines.
The client builder can be used to create http clients configured against API models using an expressive and fluent API.

```csharp
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
        
```

![Readme Image](./docs/readme_header.png)

## Table of Contents
1. [Building Clients](#Building)
2. [Request Handlers](#Request-Handlers)
3. [Request Methods](#Request-Methods)
4. [Response Types](#Response-Types)

# Building
To begin using the `IHttpClient` interface the `ClientBuilder`
static factory is used. Begin the build pipline by invoking the `ClientBuilder.CreateBuilder()`
static method. This will return an `IClientBuilder` interface directing consumers through 
each step of the build pipeline. 

### Configure Host Settings
The first step in the client builder pipline requires a host configuration. 
The `ConfigureHost` method requires an IPADRESS OR HOSTNAME as the first required parameter. 
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104")
    .CreateClient();
```
You can optionally provide a `HttpScheme` of either https or http. If no scheme is provided the builder will default to *HTTPS*
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104", scheme: HttpScheme.Http)
    .CreateClient();
```
An optional PORT can also be provided to specify a TCP port used by the HTTP or HTTPS server. 
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104", scheme: HttpScheme.Http, port: 15672)
    .CreateClient();
```
After configuring a host the builder will proceed to either the `CreateClient()` or `Authorization` steps. 

### Configure Authorization 
The client builder support several authentication steps such as: Basic Auth, 
Beaer Token, Api Keys, and finally an optional Factory. 

To add a default authorization bearer token to the client, invoke the 
`ConfigureBearerToken(token)` method. The provided token will be added to all
requests as an *Authorization: Bearer Token...* header. *Note: a future async pipeline will be provided to enable
to facilitate asynchronous authentication configuration*
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104")
    .WithBearerToken("JWT TOKEN HERE")
    .CreateClient();
```
Basic authentication is supported by invoking the `WithBasicAuth` method. 
The provided username and password will be Base64 encoded as a Basic authentication header. 
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104")
    .WithBasicAuth(username: "", password: "")
    .CreateClient();
```
Apikeys can be added to all default requests using the `WithApiKey` method. 
This method requires an APIKEY value with an option to specify a key used for the header. 
The header will default to `x-api-key` if none is provided.  
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104")
    .WithApiKey("api key", optional? header)
    .CreateClient();
```

# Request Handlers

Create a new **handler** or class that implments the `IRequestHandler` interface to handle the 
results of a specific HTTP 
![Static Badge](https://img.shields.io/badge/HTTP-GET-blue), 
![Static Badge](https://img.shields.io/badge/HTTP-POST-green), 
![Static Badge](https://img.shields.io/badge/HTTP-PUT-yellow), 
![Static Badge](https://img.shields.io/badge/HTTP-DELETE-red) request. When leveraging 
the handlers API request are initially configured and later invoked as needed. This aims to reduce
boiler plate code, prevent the need for simple methods that wrap HTTP requests, and help with 
seperation of concerns. Here our client code reads more like server code, we create requests 
and point them at handlers.   

### Creating Handlers
```csharp
public class WeatherForecastHandler : IRequestHandler
{
    public async Task HandleRequest(HttpStatusCode code, HttpResponseHeaders headers, HttpContent content)
    {
        // Called when no post request processing is required.
    }

    public async Task HandleRequest<TValue>(HttpStatusCode code, HttpResponseHeaders headers, TValue body)
    {
        // Called when post request body processing is required.
    }
}
```
### Creating Dispatchers
Create a `IHttpClient` using the `IClientBuilder` as described [above](#Building). 
```csharp
var client = ClientBuilder.CreateBuilder()
    .WithHost("172.26.6.104")
    .WithBaseRoute("api/weather")
    .BuildClient();
```

Once a new client is created you can call the `Create[VERB]Handler()` with a provided *path* and reference to 
an `IRequestHandler`. The example below creates three new ![Static Badge](https://img.shields.io/badge/HTTP-GET-blue) requests directed towards  
*https://172.26.6.104/forecast/*, *https://172.26.6.104/snow/*, and *https://172.26.6.104/rain/*.  
Each `Create[VERB]Handler` methos returns an instance of an `IDispatchRequest` interface.  

```csharp
var getWeatherRequest = client.CreateGetHandler("forecast", new WeatherForecastHandler());
var getSnowRequest = client.CreatePostHandler("snow", new WeatherForecastHandler());
var getRainRequest = client.CreateGetHandler("rain", new WeatherForecastHandler());
```
### Dispatching Requests
These `IDispatchRequest` requests can now be executed or better *dispatched* to the server by awaiting the `DispatchAsync()`.
Here we are awaiting all three tasks concurrently.
```csharp
await Task.WhenAll(
    getRainRequest.DispatchAsync(),
    getSnowRequest.DispatchAsync(new StringContent("HELLO Content")),
    getWeatherRequest.DispatchAsync());
```
### Disposing Requests
If your handlers require clean up or they have a short lifecycle you can execute the dispose method on the `IDispatchRequest`
interface.  Handlers requiring clean up should implment the `IDisposableRequestHandler` interface.  This interface will add the `IDisposable` 
members to your handler. Invoking the `IDispatchRequest.Dispose()` method will dispose you handler as well if the `IRequestHandler` 
also implements the `IDisposable` interface.
```

public class WeatherForecastHandler : IDisposableRequestHandler
{
    public async Task HandleRequest(HttpStatusCode code, HttpResponseHeaders headers, HttpContent content){}

    public async Task HandleRequest<TValue>(HttpStatusCode code, HttpResponseHeaders headers, TValue body){}

    public void Dispose()
    {
        // The handles dispose method will be invoked if you dispose of the IDispatch Request.
    }
}

// You can also create a life cycle request.
using var getUglyDWeather = client.AddGetHandler("ugly", new WeatherForecastHandler());
await getUglyDWeather.DispatchAsync();

// Your IDispatch instance will now be disposed
```

# Request Methods

### ![Static Badge](https://img.shields.io/badge/HTTP-GET-blue)

```csharp
var response = await client.GetContentFromJsonAsync<Weather>("forecast")
```

### ![Static Badge](https://img.shields.io/badge/HTTP-POST-green)

### ![Static Badge](https://img.shields.io/badge/HTTP-PUT-yellow)

### ![Static Badge](https://img.shields.io/badge/HTTP-DELETE-red)

# Response Types
All requests will return an `IResponse` interface.  This interface provides `HttpStatusCode`, Stores any handled `Exceptions` and stores a `Value<T>`
processed by the body.  An `IRespons` can only ever be one of three things, a `Success`, `Exception`, or `Error`.

## Extension Methods

#### Ensure
The Ensure method lets you safely access the nullable types stored in the `IResponse` as the callbacks will only be invoked when the result is 
already successful and stores a Value.  The ensure method then provides a predicate that will allow consumers to evealuate the data stored in the `IResponse`

```csharp
var response = await client
    .GetContentFromJsonAsync<Weather>("forecast")
    .EnsureAsync(
        predicate: (weather) => weather.IsNice && weather.Temperature > 60,
        errorFactory: () => new Exception("THE WEATHER IS NOT NICE"));
```
Ensure also provides an Async Facllback allowing consumers to access additional asynconouse resources for data validation.
```csharp
var response = await client
    .GetContentFromJsonAsync<Weather>("forecast")
    .EnsureAsync(
        predicateAsync: async (weather) => await CheckWeather(weather),
        errorFactory: () => new Exception("THE WEATHER IS NOT NICE"));
```

```csharp
var response = await client
    .GetContentFromJsonAsync<Weather>("forecast")
    .EnsureAsync(
        predicateAsync: async (weather) => await CheckWeather(weather),
        errorFactory: () => new Exception("THE WEATHER IS NOT NICE"));
```

