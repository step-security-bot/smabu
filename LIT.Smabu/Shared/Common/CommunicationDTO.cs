using LIT.Smabu.Domain.Shared.Common;

namespace LIT.Smabu.Shared.Common
{
    public class CommunicationDTO
    {
        public string Email { get; set; }
        public string Mobil { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public Communication ToValueObject()
        {
            return new Communication(Email, Mobil, Phone, Website); 
        }

        internal static CommunicationDTO Map(Communication communication)
        {
            return new CommunicationDTO
            {
                Email = communication.Email,
                Mobil = communication.Mobil,
                Phone = communication.Phone,
                Website = communication.Website
            };
        }
    }
}
