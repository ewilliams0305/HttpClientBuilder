﻿using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientBuilder;

/// <summary>
/// Provides functions that create <seealso cref="IDispatchHandler"/> handlers.
/// </summary>
public interface IDispatchHandlerFactory
{
    /// <summary>
    /// Creates a new GET request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateGetHandler(string path, IRequestHandler handler);

    /// <summary>
    /// Creates a new POST request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePostHandler(string path, IRequestHandler handler);

    /// <summary>
    /// Creates a new PUT request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePutHandler(string path, IRequestHandler handler);

    /// <summary>
    /// Creates a new DELETE request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateDeleteHandler(string path, IRequestHandler handler);
    
    /// <summary>
    /// Creates a new GET request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateGetHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) where TResponseBody : class;

    /// <summary>
    /// Creates a new POST request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePostHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) where TResponseBody : class;

    /// <summary>
    /// Creates a new PUT request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePutHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) where TResponseBody : class;

    /// <summary>
    /// Creates a new DELETE request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateDeleteHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) where TResponseBody : class;

    /// <summary>
    /// Creates a new GET request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateGetHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) where TResponseBody : class where TErrorBody : class;

    /// <summary>
    /// Creates a new POST request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePostHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) where TResponseBody : class where TErrorBody : class;

    /// <summary>
    /// Creates a new PUT request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePutHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) where TResponseBody : class where TErrorBody : class;

    /// <summary>
    /// Creates a new DELETE request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateDeleteHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) where TResponseBody : class where TErrorBody : class;


    /// <summary>
    /// Creates a new GET request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateGetHandler(string path, Func<HttpStatusCode, HttpContent, Task> handler);

    /// <summary>
    /// Creates a new POST request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePostHandler(string path, Func<HttpStatusCode, HttpContent, Task> handler);

    /// <summary>
    /// Creates a new PUT request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreatePutHandler(string path, Func<HttpStatusCode, HttpContent, Task> handler);

    /// <summary>
    /// Creates a new DELETE request dispatcher.
    /// </summary>
    /// <param name="path">The path appended to the base <seealso cref="IHttpClient"/> default request URL</param>
    /// <param name="handler">The handler to handle the HTTP request</param>
    /// <returns>A new dispatcher used to invoke the request to the server.</returns>
    IDispatchHandler CreateDeleteHandler(string path, Func<HttpStatusCode, HttpContent, Task> handler);


}