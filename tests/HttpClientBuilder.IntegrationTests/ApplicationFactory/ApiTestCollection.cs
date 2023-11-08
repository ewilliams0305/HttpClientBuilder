namespace HttpClientBuilder.IntegrationTests.ApplicationFactory;

public static class Definitions
{
    public const string WebApiCollection = "WebAPI Collection";
}

[CollectionDefinition(Definitions.WebApiCollection)]
public class ApiTestCollection : ICollectionFixture<AppFactory>
{
}