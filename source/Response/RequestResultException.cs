using System;

namespace HttpClientBuilder;

public sealed class RequestResultException: Exception  
{
    public RequestResultException(string message)
        :base(message)
    {
            
    }
}