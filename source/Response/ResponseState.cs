namespace HttpClientBuilder;

/// <summary>
/// The state of the response results.
/// </summary>
public enum ResponseState
{
    /// <summary>
    /// Response was a success and data is stored in the value.
    /// </summary>
    Success,
    /// <summary>
    /// HTTP Response was a failed status code and data is stored as an exception. 
    /// </summary>
    HttpStatusError,
    /// <summary>
    /// There was an exception during the request or response handling.  Data is stored as an exception.
    /// </summary>
    Exception
}