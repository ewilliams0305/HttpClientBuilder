
# HTTP Client Builder Testing 🔬🧪👩‍🚀🚀

The client builder solution contains two test projects as well as an ASPNET Web API Project. 
Unit tests are created for the core components of the library while integration tests are
created to test actual HTTP requests and responses.

## Table of Contents
1. [UNIT TESTS](#HttpClientBuilder.UnitTests)
2. [INTEGRATION TESTS](#HttpClientBuilder.IntegrationTests)
3. [MOCK SERVER](#HttpClientBuilder.Server)

# HttpClientBuilder.UnitTests
The unit test project includes unit tests designed to test single classes, implmenation of interfaces,
structs, and builder functions.

# HttpClientBuilder.IntegrationTests
The integration test project depends on the server project and uses the `Microsoft.AspNetCore.Mvc.Testing`
package to mock HTTP requests.  This ensures a actual client is created and actual requests => responses.

This project leverages the `WebApplicationFactory<T>` to create a mock server and mock client.
All code required to make this process function is located in the `ApplicationFactory` project folder.

The `AppFactory` class extends the webapplication and provides a few factory methods used to 
create new HTTP clients coupled to the mock server. 

*NOTE: When invoking the .CreateClient() function the WebApplication base 
CreateClient is passed to the factory overriding the HTTP client created by the builder*
```csharp
 public class AppFactory : WebApplicationFactory<IAssemblyMarker> , IAsyncLifetime
{

    public IHttpClient GetDefaultClient()
    {
        var client = ClientBuilder.CreateBuilder()
            .ConfigureHost("127.0.0.1")
            .CreateClient(CreateClient);

        return client;
    } 
    //....
}
```
This class then becomes collection fixture by leveraging XUNITs `ICollectionFixture<AppFactory>`

```csharp
public static class Definitions
{
    public const string WebApiCollection = "WebAPI Collection";
}

[CollectionDefinition(Definitions.WebApiCollection)]
public class ApiTestCollection : ICollectionFixture<AppFactory>
{
}
```

Test class can now be created with an injectable client `AppFactory` by providing a matching `Collection` attribute.
```csharp
[Collection(Definitions.WebApiCollection)]
public partial class GetContentTests
{
    protected readonly IHttpClient Client;
    
    public GetContentTests(AppFactory factory)
    {
        // Arrange
        Client = factory.GetDefaultClient();
    }
}
```
Requests can now be complete inside tests from client => server.
```csharp
// Act
var result = await Client.GetContentAsync<WeatherForecast> ("/");
```

# HttpClientBuilder.Server
This the project template created by default using minimal APIs.  The project shuould be modifed to 
tests the variose specific request type, response codes, body results, and content types.
Additional minimal APIs can be added to meet the variose testing requirements.
