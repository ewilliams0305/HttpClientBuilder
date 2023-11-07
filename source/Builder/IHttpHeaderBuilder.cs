using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc);
    }
}