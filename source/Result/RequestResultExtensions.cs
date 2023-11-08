using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientBuilder
{
    public static class ResponseResultExtensions
    {
        /// <summary>
        /// Checks the result and ensures its a successful result before invoking a predicate.
        /// If the predicate returns true, a new successful result is returned.
        /// If the predicate fails, a default error is returned.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="predicate"></param>
        /// <param name="errorFunc"></param>
        /// <returns></returns>
        public static async Task<IRequestResult<TValue>> EnsureAsync<TValue>(
            this Task<IRequestResult<TValue>> response,
            Func<TValue, Task<bool>> predicate,
            Func<TValue, Exception>? errorFunc = null)
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                return resultFromTask;
            }

            if (await predicate(resultFromTask.Value!))
            {
                return resultFromTask;
            }

            return errorFunc != null 
                ? new RequestResult<TValue>(errorFunc.Invoke(resultFromTask.Value!)) 
                : new RequestResult<TValue>(new RequestResultException($"{nameof(EnsureAsync)} Failed processing predicate value of response"));
        }

        public static async Task<IRequestResult<TValue>> HandleAsync<TValue>(
            this Task<IRequestResult<TValue>> response,
            Action<HttpStatusCode, TValue> value,
            Action<Exception> error)
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                error?.Invoke(resultFromTask.Error!);
                return resultFromTask;
            }

            value?.Invoke((HttpStatusCode)resultFromTask.StatusCode!, resultFromTask.Value!);
            return resultFromTask;
        }
    }
}