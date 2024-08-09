using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Entities;
using Jemma.Domain.ValueObjects;

namespace Jemma.Domain.Aggregates
{
    public class Item
    {
        public Guid Id {get; private set;}
        public string Name {get; private set;} = null!;
        public string Image {get; private set;} = null!;
        private readonly List<Service> _services = [];
        public IReadOnlyList<Service> Services => _services.AsReadOnly();
        public DateTime DateCreated {get; private set;} = DateTime.UtcNow;

        public static Item Create(string name, string image) {
            var item = new Item {
                Id = Guid.NewGuid(),
                Name = name,
                Image = image
            };

            return item;
        }

        public void AddService(Service service) {
            if (!_services.Contains(service)) {
                _services.Add(service);
            }
        }

        public void UpdateItemPrice(Guid serviceId, Price newPrice) {
            var service = _services.FirstOrDefault(service => service.Id == serviceId);
            service?.UpdatePrice(newPrice);
        }

        public void UpdateItem(string name, string image) {
            Name = name;
            Image = image;
        }
    }
}