using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientBuilder.Request
{
    internal sealed class RequestBuilder : IRequestBuilder, IRequestVerb, IResponseType, IRequest
    {
        private Dictionary<HttpStatusCode, Delegate>? _bodyHandlers;

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
        public IRequest MapResponse<TResponseValue>(HttpStatusCode code, Func<TResponseValue> responseTypeMap) where TResponseValue : class
        {
            _bodyHandlers ??= new Dictionary<HttpStatusCode, Delegate>();

            if(_bodyHandlers.ContainsKey(code))
            {
                throw new Exception($"{code} Status code already added to request pipeline");
            }
            _bodyHandlers.Add(code, responseTypeMap);

            return this;
        }

        #endregion

        #region Implementation of IRequest

        /// <inheritdoc />
        public Task<IResponse> Request()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}