using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Jemma.Domain.Aggregates
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Role {get; set;} = string.Empty;
    }
}