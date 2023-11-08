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
    /// <param name="result"></param>
    /// <param name="predicate"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static async Task<IResponseResult<TValue>> EnsureAsync<TValue>(
        this Task<IResponseResult<TValue>> result,
        Func<TValue, Task<bool>> predicate,
        Func<TValue, Exception> error)
    {
        var resultFromTask = await result;

        if (!resultFromTask.Success) return 
            result.Exception!;

        if (await predicate(resultFromTask.Value!))
        {
            return new IResponseResult<TValue>(resultFromTask.Value!);
        }

        return error(resultFromTask.Value!);
    }
   }

    public static async Task<IResponseResult<TValue>> HandleAsync<TValue>(
        this Task<IResponseResult<TValue>> response,
        Func<HttpStatusCode, TValue, Task<bool>> predicate,
        Func<TValue, Exception> error)
    {
        var resultFromTask = await result;

        if (!resultFromTask.Success) return 
            result.Exception!;

        if (await predicate(resultFromTask.Value!))
        {
            return new IResponseResult<TValue>(resultFromTask.Value!);
        }

        return error(resultFromTask.Value!);
    }
   }



}