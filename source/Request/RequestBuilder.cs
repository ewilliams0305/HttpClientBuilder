using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientBuilder.Request
{
    internal sealed class RequestBuilder : IRequestBuilder, IRequestVerb, IResponseType, IRequest
    {
        private Dictionary<HttpStatusCode, Func<HttpStatusCode, Stream, Task<IRequestResult<TValue>>>? _bodyHandlers;

        #region Implementation of IRequestBuilder

        /// <inheritdoc />
        public IRequestVerb Make()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IRequestVerb

        /// <inheritdoc />
        public IResponseType Get(string? url = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IResponseType Put(string? url = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IResponseType Post(string? url = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IResponseType Delete(string? url = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IResponseMap

        /// <inheritdoc />
        public IRequest MapResponse<TResponseValue>(HttpStatusCode code, Func<Content, TResponseValue> responseTypeMap) where TResponseValue : class
        { 
            if(_bodyHandlers == null)
            {
                _bodyHandlers = new();
            }
            if(_bodyHandlers.ContainsKey(code))
            {
                throw new Exception("Sttus code already added to pipline");
            }
            _bodyHandlers.Add(code, function);
        }

        #endregion

        #region Implementation of IRequest

        /// <inheritdoc />
        public Task<IRequestResult> Request()
        {
            var response = await _client.Get("");
            
            if(_bodyHandler.ContainsKey(response.StatusCode))
            {
                var obj = await _bodyHandler[code].Invoke(code);
            }
        }

        #endregion
    }
}