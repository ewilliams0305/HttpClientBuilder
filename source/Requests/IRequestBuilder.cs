namespace HttpClientBuilder.Request;

/// <summary>
/// Starts a request builder pipeline
/// </summary>
public interface IRequestBuilder
{
    /// <summary>
    /// Creates the new request
    /// </summary>
    /// <returns>Next pipeline step</returns>
    IRequestVerb Make();
}