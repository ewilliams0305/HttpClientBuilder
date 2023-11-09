using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientBuilder
{
    /// <summary>
    /// Extension methods used to process the results of a http request <seealso cref="IRequestResult{TSuccessValue}"/>
    /// </summary>
    public static class RequestResultExtensions
    {
        /// <summary>
        /// Checks the result and ensures its a successful result before invoking a predicate.
        /// If the predicate returns true, a new successful result is returned.
        /// If the predicate fails, a default error is returned.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="predicate"></param>
        /// <param name="errorFactory"></param>
        /// <returns></returns>
        public static IRequestResult<TValue> Ensure<TValue>(
            this IRequestResult<TValue> response,
            Func<TValue, bool> predicate,
            Func<Exception>? errorFactory = null) where TValue : class
        {
            if (!response.Success)
            {
                return response;
            }

            if (predicate(response.Value!))
            {
                return response;
            }

            return errorFactory != null
                ? new RequestResult<TValue>(errorFactory.Invoke())
                : new RequestResult<TValue>(new RequestResultException($"{nameof(Ensure)} Failed processing predicate value of response"));
        }

        /// <summary>
        /// Checks the result and ensures its a successful result before invoking a predicate.
        /// If the predicate returns true, a new successful result is returned.
        /// If the predicate fails, a default error is returned.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="predicate"></param>
        /// <param name="errorFactory"></param>
        /// <returns></returns>
        public static async Task<IRequestResult<TValue>> EnsureAsync<TValue>(
            this Task<IRequestResult<TValue>> response,
            Func<TValue, bool> predicate,
            Func<Exception>? errorFactory = null) where TValue : class
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                return resultFromTask;
            }

            if (predicate(resultFromTask.Value!))
            {
                return resultFromTask;
            }

            return errorFactory != null 
                ? new RequestResult<TValue>(errorFactory.Invoke()) 
                : new RequestResult<TValue>(new RequestResultException($"{nameof(EnsureAsync)} Failed processing predicate value of response"));
        }

        /// <summary>
        /// Checks the result and ensures its a successful result before invoking a predicate.
        /// If the predicate returns true, a new successful result is returned.
        /// If the predicate fails, a default error is returned.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="predicateAsync"></param>
        /// <param name="errorFactory"></param>
        /// <returns></returns>
        public static async Task<IRequestResult<TValue>> EnsureAsync<TValue>(
            this Task<IRequestResult<TValue>> response,
            Func<TValue, Task<bool>> predicateAsync,
            Func<Exception>? errorFactory = null) where TValue : class
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                return resultFromTask;
            }

            if (await predicateAsync(resultFromTask.Value!))
            {
                return resultFromTask;
            }

            return errorFactory != null 
                ? new RequestResult<TValue>(errorFactory.Invoke()) 
                : new RequestResult<TValue>(new RequestResultException($"{nameof(EnsureAsync)} Failed processing predicate value of response"));
        }
        
        
        /// <summary>
        /// Processes the response by providing two functions.
        /// If the result was successful then value() will invoke.
        /// If the result was a failure then the error() function will invoke.
        /// This method guarantees the values passed to the functions are NEVER NULL 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="value"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static IRequestResult<TValue> Handle<TValue>(
            this IRequestResult<TValue> response,
            Action<HttpStatusCode, TValue> value,
            Action<Exception> error) where TValue : class
        {
            if (!response.Success)
            {
                error?.Invoke(response.Error!);
                return response;
            }

            value?.Invoke((HttpStatusCode)response.StatusCode!, response.Value!);
            return response;
        }

        /// <summary>
        /// Processes the response by providing two functions.
        /// If the result was successful then value() will invoke.
        /// If the result was a failure then the error() function will invoke.
        /// This method guarantees the values passed to the functions are NEVER NULL 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="value"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static async Task<IRequestResult<TValue>> HandleAsync<TValue>(
            this Task<IRequestResult<TValue>> response,
            Action<HttpStatusCode, TValue> value,
            Action<Exception> error) where TValue : class
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