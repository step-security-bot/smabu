using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.Common
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
        public static Communication Empty => new("", "", "", "");
    }
}
