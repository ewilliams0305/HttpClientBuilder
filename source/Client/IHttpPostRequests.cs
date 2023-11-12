using System;
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
    /// <param name="body">Body and header body to add to the request.</param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse> PostAsync(string route = "", HttpContent? body = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequestBody"></typeparam>
    /// <param name="route"></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IResponse> PostAsync<TRequestBody>(string route = "", TRequestBody? body = default, CancellationToken cancellationToken = default) where TRequestBody : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route and deserializes the body as the specified return type. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="body"></param>
    /// <param name="context">Optional JSON Deserializer context to leverage source generated deserializers.</param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessType>> PostContentFromJsonAsync<TSuccessType>(string route = "", HttpContent? body = default, JsonSerializerContext? context = null, CancellationToken cancellationToken = default) where TSuccessType : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSuccessType"></typeparam>
    /// <typeparam name="TRequestBody"></typeparam>
    /// <param name="route"></param>
    /// <param name="body"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IResponse<TSuccessType>> PostContentFromJsonAsync<TSuccessType, TRequestBody>(string route = "", TRequestBody? body = null, JsonSerializerContext? context = null, CancellationToken cancellationToken = default) where TSuccessType : class where TRequestBody : class;

    /// <summary>
    /// Executes a POST request asynchronously at the specified route and deserializes the body as the specified return type. 
    /// </summary>
    /// <param name="route">The specified route will be appended to the <seealso cref="IClientBuilder"/> default request <seealso cref="Uri"/></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Optional cancellation token to stop a request in-flight.</param>
    /// <returns>Result of the executed request</returns>
    Task<IResponse<TSuccessBody, TErrorBody>> PostContentFromJsonAsync<TSuccessBody, TErrorBody>(string route = "", HttpContent? body = default, CancellationToken cancellationToken = default) 
        where TSuccessBody : class 
        where TErrorBody : class ;
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSuccessBody"></typeparam>
    /// <typeparam name="TErrorBody"></typeparam>
    /// <typeparam name="TRequestBody"></typeparam>
    /// <param name="route"></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IResponse<TSuccessBody, TErrorBody>> PostContentFromJsonAsync<TSuccessBody, TErrorBody, TRequestBody>(string route = "", TRequestBody? body = default, CancellationToken cancellationToken = default) 
        where TSuccessBody : class 
        where TErrorBody : class
        where TRequestBody : class ;

}

