using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jemma.Domain.Entities
{
    public class Customer
    {
        public Guid Id {get; set;}
        public string Name {get; private set;} = null!;
        public string Contact {get; private set;} = null!;
        public string Address {get; private set;} = null!;
        public DateTime DateCreated {get; private set;} = DateTime.UtcNow;

        public static Customer Create(string name, string contact, string address) {
            var customer = new Customer {
                Id = Guid.NewGuid(),
                Name = name,
                Contact = contact,
                Address = address
            };

            return customer;
        }
    }
}