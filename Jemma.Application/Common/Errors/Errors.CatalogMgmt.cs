using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Jemma.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class Catalog {
            public static Error Conflict => Error.Conflict(code: "Catalog.Conflict", description: "Item already exists"); 
            public static Error NotFound => Error.NotFound(code: "Catalog.NotFound", description: "Item not found"); 
        }
    }
}