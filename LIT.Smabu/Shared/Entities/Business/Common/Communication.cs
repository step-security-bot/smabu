using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Entities.Business.Common
{
    public record Communication : IValueObject
    {
        public Communication(string email, string mobil, string phone, string website)
        {
            Email = email;
            Mobil = mobil;
            Phone = phone;
            Website = website;
        }

        public string Email { get; private set; }
        public string Mobil { get; private set; }
        public string Phone { get; private set; }
        public string Website { get; private set; }
    }
}
