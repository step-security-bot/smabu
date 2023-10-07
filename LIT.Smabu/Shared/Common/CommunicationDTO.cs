using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Common
{
    public class CommunicationDTO : IDTO
    {
        public string Email { get; set; }
        public string Mobil { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
