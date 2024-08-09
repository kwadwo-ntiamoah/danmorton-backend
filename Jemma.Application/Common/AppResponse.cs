using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Jemma.Application.Common
{
    public class AppResponse<T>
    {
        public string Message {get; set;} = string.Empty;
        public HttpStatusCode Status {get; set;}
        public T? Data {get; set;}
    }
}