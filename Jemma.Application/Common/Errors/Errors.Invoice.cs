using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace Jemma.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class Invoice {
            public static Error Conflict => Error.Conflict(code: "Invoice.Conflict", description: "Invoice already exists"); 
            public static Error NotFound => Error.NotFound(code: "Invoice.NotFound", description: "Invoice not found"); 
            public static Error OverPosting(decimal AmountLeft) => Error.Forbidden(code: "Invoice.OverPosting", description: $"You cannot pay more than {AmountLeft}");
        }
    }
}