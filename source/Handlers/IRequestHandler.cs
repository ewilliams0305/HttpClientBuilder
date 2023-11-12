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
        /// <param name="content"></param>
        /// <returns></returns>
        Task HandleRequest(HttpStatusCode code, HttpContent content);
    }

    /// <summary>
    /// A request handler that accepts a precomputed parsed body.
    /// </summary>
    /// <typeparam name="TResponseBody">Type of object to convert the body or the content too.</typeparam>
    public interface IRequestHandler<in TResponseBody> where TResponseBody : class
    {
        /// <summary>
        /// Handles a response from a server with a precomputed body Type.
        /// When the request is dispatched the body will parsed from JSON and sent to the handler as a type.
        /// </summary>
        /// <param name="code">Http Status Code</param>
        /// <param name="headers">Response headers</param>
        /// <param name="content">Content body as strongly typed class</param>
        /// <returns>asynchronous task.</returns>
        Task HandleBody(HttpStatusCode code, HttpResponseHeaders headers, TResponseBody content);

        /// <summary>
        /// Should an exception occur requesting the server, parsing the JSON. the request will be returned to the handler as an exception.
        /// </summary>
        /// <param name="exception">Exception or reason the failure occurred.</param>
        /// <returns>exception task.</returns>
        Task HandleException(Exception exception);
    }

    /// <summary>
    /// A request handler that accepts a precomputed parsed body.
    /// This handler adds the ability to precompute the error body as well.
    /// Status codes >= 400 will be processed with the TResponseError type.
    /// </summary>
    /// <typeparam name="TResponseBody">Type of object to convert the body or the content too.</typeparam>
    /// <typeparam name="TResponseError">Type of object parsed when the code is a failure.</typeparam>
    public interface IRequestHandler<in TResponseBody, in TResponseError> where TResponseBody : class
    {
        /// <summary>
        /// Handles a response from a server with a precomputed body Type.
        /// When the request is dispatched the body will parsed from JSON and sent to the handler as a type.
        /// </summary>
        /// <param name="code">Http Status Code</param>
        /// <param name="headers">Response headers</param>
        /// <param name="content">Content body as strongly typed class</param>
        /// <returns>asynchronous task.</returns>
        Task HandleBody(HttpStatusCode code, HttpResponseHeaders headers, TResponseBody content);

        /// <summary>
        /// Handles a error response from a server with a precomputed body Type.
        /// When the request is dispatched if the server responds with a error code the message will be sent to the handler as an error.
        /// </summary>
        /// <param name="code">Http Status Code</param>
        /// <param name="headers">Response headers</param>
        /// <param name="content">Content body as strongly typed class</param>
        /// <returns>asynchronous task.</returns>
        Task HandleError(HttpStatusCode code, HttpResponseHeaders headers, TResponseError content);

        /// <summary>
        /// Should an exception occur requesting the server, parsing the JSON. the request will be returned to the handler as an exception.
        /// </summary>
        /// <param name="exception">Exception or reason the failure occurred.</param>
        /// <returns>exception task.</returns>
        Task HandleException(Exception exception);
    }

    /// <summary>
    /// Adds a disposable method to the handler
    /// </summary>
    public interface IDisposableRequestHandler : IRequestHandler, IDisposable
    {

    }

    /// <summary>
    /// Adds a disposable method to the handler
    /// </summary>
    /// <typeparam name="TResponseBody"></typeparam>
    /// <seealso cref="IRequestHandler"/>
    public interface IDisposableRequestHandler<in TResponseBody> : 
        IRequestHandler<TResponseBody>, IDisposable 
        where TResponseBody : class
    {
    }

    /// <summary>
    /// Adds a disposable method to the handler
    /// </summary>
    /// <typeparam name="TResponseBody"></typeparam>
    /// <typeparam name="TResponseError"></typeparam>
    /// <seealso cref="IRequestHandler"/>
    public interface IDisposableRequestHandler<in TResponseBody, in TResponseError> : 
        IRequestHandler<TResponseBody, TResponseError>, IDisposable 
        where TResponseBody : class 
        where TResponseError : class
    {
    }
}
