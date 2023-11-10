using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientBuilder
{
    /// <summary>
    /// Handles a response from a HTTP request
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Handles a response with no content processing.  Data will be dispatched as it was received from the server.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task HandleRequest(HttpStatusCode code, HttpResponseHeaders headers, HttpContent content);

        /// <summary>
        /// Handles a response with content processing.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task HandleRequest<TValue>(HttpStatusCode code, HttpResponseHeaders headers, TValue body);
    }

    /// <summary>
    /// Adds a disposable method to the handler
    /// </summary>
    public interface IDisposableRequestHandler : IRequestHandler, IDisposable
    {

    }
}
