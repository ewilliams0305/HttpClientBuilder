using HttpClientBuilder.Request;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientBuilder
{
    public interface IHttpClient : IHttpGetRequests, IDisposable
    {
        Uri BaseAddress { get; }

        AuthenticationHeaderValue AuthenticationHeader { get; }

        HttpRequestHeaders RequestHeaders { get; }

        IRequestBuilder CreateRequest();
    }

    public interface IHttpGetRequests
    {
        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route = "/", CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, TSuccessType?> createResultFromContent, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, Task<TSuccessType?>> createResultFromContentAsync, CancellationToken cancellationToken = default);
        
        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], TSuccessType?> createResultFromBytes, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], Task<TSuccessType?>> createResultFromBytesAsync, CancellationToken cancellationToken = default);

        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, TSuccessType?> createResultFromStream, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, Task<TSuccessType?>> createResultFromStreamAsync, CancellationToken cancellationToken = default);
    }


}