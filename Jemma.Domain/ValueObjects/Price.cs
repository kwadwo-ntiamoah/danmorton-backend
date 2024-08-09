using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jemma.Domain.ValueObjects
{
    public record Price(decimal Amount, string Currency = "GHS");
}