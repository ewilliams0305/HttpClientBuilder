using System;
using System.Collections.Generic;

namespace HttpClientBuilder
{
    /// <summary>
    /// Appends headers or advances the pipeline to the build method.
    /// </summary>
    public interface IHeaderOrBuilder : IHeaderOption, IClientBuilder
    {

    }

    /// <summary>
    /// Appends customer header to the pipeline and advances to either the <seealso cref="IClientBuilder"/> step or additional headers.
    /// </summary>
    public interface IHttpHeaderBuilder : IHeaderOption
    {

    }

    /// <summary>
    /// Appends a header to the client.
    /// </summary>
    public interface IHeaderOption
    {
        /// <summary>
        /// Appends a key value pair to the Http Client.
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header value</param>
        /// <returns>Optional pipeline step to append additional headers</returns>
        IHeaderOrBuilder WithHeader(string key, string value);

        /// <summary>
        /// Appends a key value pair to the Http Client returned from a function.
        /// </summary>
        /// <param name="headerFunc">Header key</param>
        /// <returns>Optional pipeline step to append additional headers</returns>
        IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc);
    }
}