using HttpClientBuilder.Model;

namespace HttpClientBuilder.Examples
{
    internal class BuilderExamples
    {
        public BuilderExamples()
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("172.26.6.104")
                .ConfigureBearerToken("JWT TOKEN HERE")
                .AcceptSelfSignedCerts()
                .WithHeader("x-api-key", "this is an extra header")
                .CreateClient();

            client.CreateRequest()
                .Make()
                .Get()
                .MapResponse((code) => typeof(BuilderConfiguration))
                .Request();
        }
    }
}
