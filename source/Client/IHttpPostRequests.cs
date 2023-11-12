using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientBuilder;

/// <summary>
/// Provides all the required function for Http Post requests.
/// </summary>
public interface IHttpPostRequests
{
    /// <summary>
    /// Executes a POST request asynchronously at the specified route. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse> PostAsync(string route = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a POST request asynchronously at the specified route and deserializes the content as the specified return type. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="context">Optional JSON Deserializer context to leverage source generated deserializers.</param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentFromJsonAsync<TSuccessType>(string route = "", JsonSerializerContext? context = null, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route and deserializes the content as the specified return type. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessBody, TErrorBody>> PostContentFromJsonAsync<TSuccessBody, TErrorBody>(string route = "", CancellationToken cancellationToken = default) 
        where TSuccessBody : class 
        where TErrorBody : class ;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route.
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="createResultFromContent"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, TSuccessType?> createResultFromContent, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="createResultFromContentAsync"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, Task<TSuccessType?>> createResultFromContentAsync, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="createResultFromBytes"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], TSuccessType?> createResultFromBytes, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="createResultFromBytesAsync"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], Task<TSuccessType?>> createResultFromBytesAsync, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="createResultFromStream"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, TSuccessType?> createResultFromStream, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="createResultFromStreamAsync"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, Task<TSuccessType?>> createResultFromStreamAsync, CancellationToken cancellationToken = default) where TSuccessType : class;
}

