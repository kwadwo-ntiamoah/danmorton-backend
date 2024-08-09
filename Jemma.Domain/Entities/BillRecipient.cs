using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jemma.Domain.Entities
{
    public class BillRecipient
    {
        public string Name {get; private set;} = null!;
        public string Contact {get; private set;} = null!;
        public string Address {get; private set;} = null!;

        public static BillRecipient Create(string name, string contact, string address) {
            var recipient = new BillRecipient {
                Name = name,
                Contact = contact,
                Address = address
            };

            return recipient;
        }
    }
}