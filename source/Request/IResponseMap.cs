using System;
using System.Net;

namespace HttpClientBuilder.Request;

/// <summary>
/// Maps a status code to a result object type
/// </summary>
public interface IResponseMap
{
    /// <summary>
    /// Maps a code => type
    /// </summary>
    /// <param name="responseTypeMap"></param>
    /// <returns></returns>
    IRequest MapResponse<TResponseContent>(Func<HttpStatusCode, TResponseContent> responseTypeMap) where TResponseContent : class;
}