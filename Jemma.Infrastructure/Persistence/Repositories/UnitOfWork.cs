
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Infrastructure.Persistence.Configurations;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Item = new ItemRepository(_context);
            Service = new ServiceRepository(_context);
            Product = new ProductRepository(_context);
            Payment = new PaymentRepository(_context);
            Invoice = new InvoiceRepository(_context);
            InvoiceItem = new InvoiceItemRepository(_context);
            Customer = new CustomerRepository(_context);
            Basket = new BasketRepository(_context);
            BasketItem = new BasketItemRepository(_context);
        }

        public IItemRepository Item {get; private set; } = null!;
        public IServiceRepository Service {get; private set;} = null!;
        public IProductRepository Product {get; private set;} = null!;
        public IPaymentRepository Payment {get; private set;} = null!;
        public IInvoiceRepository Invoice {get; private set;} = null!;
        public IInvoiceItemRepository InvoiceItem {get; private set;} = null!;
        public ICustomerRepository Customer {get; private set;} = null!;
        public IBasketRepository Basket {get; private set;} = null!;
        public IBasketItemRepository BasketItem {get; private set;} = null!;

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}