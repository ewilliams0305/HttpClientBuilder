using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientBuilder.Request
{
    internal sealed class RequestBuilder : IRequestBuilder, IRequestVerb, IResponseType, IRequest
    {
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
        public IRequest MapResponse<TResponseValue>(Func<HttpStatusCode, TResponseValue> responseTypeMap) where TResponseValue : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IRequest

        /// <inheritdoc />
        public async Task Request()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IResponseMap

        /// <inheritdoc />
        public IRequest MapResponse(Func<HttpStatusCode, Type> responseTypeMap)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    public interface IRequestBuilder
    {
        IRequestVerb Make();
    }

    public interface IRequestVerb
    {
        IResponseType Get(string? url = default);
        IResponseType Put(string? url = default);
        IResponseType Post(string? url = default);
        IResponseType Delete(string? url = default);
    }

    public interface IResponseType : IResponseMap
    {

    }

    public interface IResponseMap
    {
        IRequest MapResponse(Func<HttpStatusCode, Type> responseTypeMap);
    }
    public interface IHandleExceptionMap
    {
        IRequest HandleException<TExceptionType>(Func<HttpStatusCode, TExceptionType> responseTypeMap) where TExceptionType : Exception;
    }

    public interface IRequest
    {
        Task Request();
    }
}