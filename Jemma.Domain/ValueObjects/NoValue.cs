using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jemma.Domain.ValueObjects
{
    public class NoValue() {
        public static NoValue New() {
            return new NoValue();
        }
    }
}