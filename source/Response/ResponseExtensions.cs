using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientBuilder
{
    /// <summary>
    /// Extension methods used to process the results of a http request <seealso cref="IResponse{TSuccessValue}"/>
    /// </summary>
    public static class ResponseExtensions
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
        public static IResponse<TValue> Ensure<TValue>(
            this IResponse<TValue> response,
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
                ? new Response<TValue>(errorFactory.Invoke())
                : new Response<TValue>(new RequestResultException($"{nameof(Ensure)} Failed processing predicate value of response"));
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
        public static async Task<IResponse<TValue>> EnsureAsync<TValue>(
            this Task<IResponse<TValue>> response,
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
                ? new Response<TValue>(errorFactory.Invoke()) 
                : new Response<TValue>(new RequestResultException($"{nameof(EnsureAsync)} Failed processing predicate value of response"));
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
        public static async Task<IResponse<TValue>> EnsureAsync<TValue>(
            this Task<IResponse<TValue>> response,
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
                ? new Response<TValue>(errorFactory.Invoke()) 
                : new Response<TValue>(new RequestResultException($"{nameof(EnsureAsync)} Failed processing predicate value of response"));
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
        public static IResponse<TValue> EnsureHeaders<TValue>(
            this IResponse<TValue> response,
            Func<HttpResponseHeaders, bool> predicate,
            Func<Exception>? errorFactory = null) where TValue : class
        {
            if (!response.Success)
            {
                return response;
            }

            if (predicate(response.Headers!))
            {
                return response;
            }

            return errorFactory != null
                ? new Response<TValue>(errorFactory.Invoke())
                : new Response<TValue>(new RequestResultException($"{nameof(Ensure)} Failed processing headers predicate"));
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
        public static async Task<IResponse<TValue>> EnsureHeadersAsync<TValue>(
            this Task<IResponse<TValue>> response,
            Func<HttpResponseHeaders, bool> predicate,
            Func<Exception>? errorFactory = null) where TValue : class
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                return resultFromTask;
            }

            if (predicate(resultFromTask.Headers!))
            {
                return resultFromTask;
            }

            return errorFactory != null
                ? new Response<TValue>(errorFactory.Invoke())
                : new Response<TValue>(new RequestResultException($"{nameof(EnsureAsync)} Failed processing headers predicate"));
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
        public static IResponse<TValue> Handle<TValue>(
            this IResponse<TValue> response,
            Action<HttpStatusCode, HttpResponseHeaders, TValue> value,
            Action<Exception> error) where TValue : class
        {
            if (!response.Success)
            {
                error?.Invoke(response.Error!);
                return response;
            }

            try
            {
                value.Invoke((HttpStatusCode)response.StatusCode!, response.Headers!, response.Value!);
                return response;
            }
            catch (Exception e)
            {
                return new Response<TValue>(e);
            }
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
        /// <returns>The same result unless an exception occurs processing the value.</returns>
        public static async Task<IResponse<TValue>> HandleAsync<TValue>(
            this Task<IResponse<TValue>> response,
            Action<HttpStatusCode, HttpResponseHeaders, TValue> value,
            Action<Exception> error) where TValue : class
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                error?.Invoke(resultFromTask.Error!);
                return resultFromTask;
            }

            try
            {
                value.Invoke((HttpStatusCode)resultFromTask.StatusCode!, resultFromTask.Headers!, resultFromTask.Value!);
                return resultFromTask;
            }
            catch (Exception e)
            {
                return new Response<TValue>(e);
            }
        }

        /// <summary>
        /// Processes the response by providing two functions.
        /// If the result was successful then value() will invoke.
        /// If the result was a failure then the error() function will invoke.
        /// This method guarantees the values passed to the functions are NEVER NULL 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="response"></param>
        /// <param name="valueAsync"></param>
        /// <param name="errorAsync"></param>
        /// <returns>The same result unless an exception occurs processing the value.</returns>
        public static async Task<IResponse<TValue>> HandleAsync<TValue>(
            this Task<IResponse<TValue>> response,
            Func<HttpStatusCode, HttpResponseHeaders, TValue, Task> valueAsync,
            Func<Exception, Task> errorAsync) where TValue : class
        {
            var resultFromTask = await response;

            if (!resultFromTask.Success)
            {
                await errorAsync.Invoke(resultFromTask.Error!);
                return resultFromTask;
            }

            try
            {
                await valueAsync.Invoke((HttpStatusCode)resultFromTask.StatusCode!, resultFromTask.Headers!, resultFromTask.Value!);
                return resultFromTask;
            }
            catch (Exception e)
            {
                return new Response<TValue>(e);
            }
        }
        
        public static async Task<IResponse<TValue>> HandleAsync<TValue, TError>(
            this Task<IResponse<TValue, TError>> response,
            Func<HttpStatusCode, HttpResponseHeaders, TValue, Task> valueAsync,
            Func<HttpStatusCode, HttpResponseHeaders, TError, Task> errorAsync,
            Func<Exception, Task> exceptionAsync) 
            where TValue : class
            where TError : class
        {
            var resultFromTask = await response;

            try
            {
                switch (resultFromTask.Status)
                {
                    case ResponseState.Success:
                        await valueAsync.Invoke((HttpStatusCode)resultFromTask.StatusCode!, resultFromTask.Headers!, resultFromTask.Value!);
                        break;
                    case ResponseState.HttpStatusError:
                        await errorAsync.Invoke((HttpStatusCode)resultFromTask.StatusCode!, resultFromTask.Headers!, resultFromTask.ErrorValue!);
                        break;
                    case ResponseState.Exception:
                        await exceptionAsync.Invoke(resultFromTask.Error!);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return resultFromTask;
            }
            catch (Exception e)
            {
                return new Response<TValue>(e);
            }
        }
    }
}