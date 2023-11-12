namespace HttpClientBuilder;

internal sealed partial class HttpBuilderClient
{
    #region Implementation of IHttpHandleRequests

    /// <inheritdoc />
    public IDispatchHandler CreateGetHandler(string path, IRequestHandler handler) => new DispatchHandlerWithoutBody(path, _client.GetAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreatePostHandler(string path, IRequestHandler handler) => new DispatchHandlerWithBody(path, _client.PostAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreatePutHandler(string path, IRequestHandler handler) => new DispatchHandlerWithBody(path, _client.PutAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreateDeleteHandler(string path, IRequestHandler handler) => new DispatchHandlerWithoutBody(path, _client.DeleteAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreateGetHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePostHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
	{
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePutHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
	{
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreateDeleteHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
	{
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreateGetHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePostHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePutHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public IDispatchHandler CreateDeleteHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        throw new System.NotImplementedException();
    }

    #endregion
}