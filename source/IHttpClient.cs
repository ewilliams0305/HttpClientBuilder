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
        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, TSuccessType?> createResultFunc, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, Task<TSuccessType?>> createResultFuncAsync, CancellationToken cancellationToken = default);
        
        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], TSuccessType?> createResultFunc, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], Task<TSuccessType?>> createResultFuncAsync, CancellationToken cancellationToken = default);

        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, TSuccessType?> createResultFunc, CancellationToken cancellationToken = default);
        Task<IRequestResult<TSuccessType>> GetAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, Task<TSuccessType?>> createResultFuncAsync, CancellationToken cancellationToken = default);
    }


}