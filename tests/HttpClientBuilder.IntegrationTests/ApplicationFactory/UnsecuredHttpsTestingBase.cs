namespace HttpClientBuilder.IntegrationTests.ApplicationFactory;

[Collection(Definitions.WebApiCollection)]
public partial class UnsecuredHttpsTestingBase
{
    protected readonly IHttpClient Client;
    public UnsecuredHttpsTestingBase(AppFactory factory)
    {
        Client = factory.GetPrefixedClient();
    }
}