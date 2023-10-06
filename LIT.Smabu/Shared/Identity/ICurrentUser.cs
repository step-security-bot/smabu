using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Identity
{
    public interface ICurrentUser
    {
        string Id { get; }
        string Name { get; }
    }
}
