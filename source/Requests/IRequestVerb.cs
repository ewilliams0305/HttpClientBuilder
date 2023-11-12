namespace HttpClientBuilder;

/// <summary>
/// Specifies the request verb
/// </summary>
public interface IRequestVerb
{
    /// <summary>
    /// HTTP GET Request Verb
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IResponseType Get(string? url = default);
        
    /// <summary>
    /// HTTP POST Request Verb
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IResponseType Post(string? url = default);
        
    /// <summary>
    /// HTTP PUT Request Verb
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IResponseType Put(string? url = default);
        
    /// <summary>
    /// HTTP DELETE Request Verb
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IResponseType Delete(string? url = default);
}