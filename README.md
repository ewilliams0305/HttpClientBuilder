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
IHttpClient client = ClientBuilder.CreateBuilder()
    .ConfigureHost("172.26.6.104")
    .ConfigureBearerToken("JWT TOKEN HERE")
    .AcceptSelfSignedCerts()
    .WithHeader("x-api-key", "this is an extra header")
    .CreateClient();

public async Task<bool> ExampleGetWeatherAndCheckIfItsNice()
{
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
```

![Readme Image](./docs/readme_header.png)

## Table of Contents
1. [Building Clients](#Building)
2. [Expressions](#Expressions)
3. [Requests](#Requests)
4. [Responses](#Responses)

# Building
To begin using the `IHttpClient` interface the `ClientBuilder`
must be used. Begin the build pipline by involing the `ClientBuilder.CreateBuilder()`
static factory. This will return an `IClientBuilder` interface directing consumers through 
each step of the build pipeline. 

### Configure Host Settings
The first step in the client builder pipline requires a host configuration. 
The `ConfigureHost` method requires an IPADRESS OR HOSTNAME as the first required parameter. 
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .ConfigureHost("172.26.6.104")
    .CreateClient();
```
You can optionally provide a `HttpScheme` of either https or http. If no scheme is provided the builder will default to *HTTPS*
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .ConfigureHost("172.26.6.104", scheme: HttpScheme.Http)
    .CreateClient();
```
An optional PORT can also be provided to specify a TCP port used by the HTTP or HTTPS server. 
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .ConfigureHost("172.26.6.104", scheme: HttpScheme.Http, port: 15672)
    .CreateClient();
```
After configuring a host the builder will proceed to either the `CreateClient()` or `Authorization` steps. 

### Configure Authorization 
The client builder support several authentication steps such as: Basic Auth, 
Beaer Token, Api Keys, and finally an optional Factory. 

To add a default authorization bearer token to the client, invoke the 
`ConfigureBearerToken(token)` method. The provided token will be added to all
requests an *Authorization: Bearer Token...* header. 
```csharp
IHttpClient client = ClientBuilder.CreateBuilder()
    .ConfigureHost("172.26.6.104")
    .ConfigureBearerToken("JWT TOKEN HERE")
    .CreateClient();
```

# Expressions

# Requests

# Responses
