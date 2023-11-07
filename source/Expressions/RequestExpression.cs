using System;
using System.Collections.Generic;
using System.Text;

namespace ClientBuilder.Expressions
{
    public sealed class RequestExpression<TSuccessResponse, TErrorResponse> 
        where TSuccessResponse : class 
        where TErrorResponse : class
    {

        public RequestExpression()
        {
            
        }
    }
}
