using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Commands
{
    public class CreateCustomer
    {
        public CreateCustomer(string name1, string name2)
        {
            Name1 = name1;
            Name2 = name2;
        }

        public string Name1 { get; }
        public string Name2 { get; }
    }
}
