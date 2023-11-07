namespace ClientBuilder
{
    public interface IHeaderOrBuilder : IHeaderOption, IClientBuilder
    {

    }

    public interface IHttpHeaderBuilder : IHeaderOption
    {

    }

    public interface IHeaderOption
    {
        IHeaderOrBuilder WithHeader(string key, string value);
    }
}