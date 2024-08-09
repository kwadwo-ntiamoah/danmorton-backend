using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jemma.Application.Common.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        public IItemRepository Item {get; }
        public IServiceRepository Service {get;}
        public IProductRepository Product {get;}
        public IPaymentRepository Payment {get;}
        public IInvoiceRepository Invoice {get;}
        public IInvoiceItemRepository InvoiceItem {get;}
        public ICustomerRepository Customer {get;}
        public IBasketRepository Basket {get;}
        public IBasketItemRepository BasketItem {get;}

        public Task CommitAsync();
    }
}