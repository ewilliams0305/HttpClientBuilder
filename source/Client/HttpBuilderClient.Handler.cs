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

    #endregion
}