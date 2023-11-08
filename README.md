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
1. [Building](#Building)
2. [Expressions](#Expressions)
3. [Requests](#Requests)
4. [Responses](#Responses)

# Building

# Expressions

# Requests

# Responses
