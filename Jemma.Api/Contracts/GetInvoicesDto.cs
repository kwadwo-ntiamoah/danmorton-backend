using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Enums;

namespace Jemma.Api.Contracts
{
    public class GetInvoicesDto
    {
        public Guid? InvoiceId {get; set;}
        public string? CustomerName {get; set;}
        public InvoicePaymentStatus? Status {get; set;}
        public DateTime? Date {get; set;}
    }
}