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
    .ConfigureHost("172.26.6.104")
    .ConfigureBearerToken("JWT TOKEN HERE")
    .AcceptSelfSignedCerts()
    .WithHeader("x-api-key", "this is an extra header")
    .CreateClient();

await client.MakeRequest(r => ...)
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
