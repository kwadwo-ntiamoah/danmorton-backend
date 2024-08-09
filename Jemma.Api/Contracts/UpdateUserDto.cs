using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Jemma.Api.Contracts
{
    public class UpdateUserDto
    {
        public string? UserName {get; set;}
        public string FullName { get; set; } = string.Empty;
        public string Role {get; set;} = string.Empty;
    }
}