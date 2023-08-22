using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class SmabuException : System.Exception
    {
        public SmabuException(string message) : base(message) { }
    }
}
