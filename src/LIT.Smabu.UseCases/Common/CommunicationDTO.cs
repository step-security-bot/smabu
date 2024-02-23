using LIT.Smabu.Domain.Common;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Common
{
    public class CommunicationDTO : IDTO
    {
        public string DisplayName => "";
        public string Email { get; set; }
        public string Mobil { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }


        public static CommunicationDTO From(Communication communication)
        {
            return new CommunicationDTO()
            {
                Email = communication.Email,
                Mobil = communication.Mobil,
                Phone = communication.Phone,
                Website = communication.Website
            };
        }
    }
}
