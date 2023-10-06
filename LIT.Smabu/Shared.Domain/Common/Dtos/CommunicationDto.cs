namespace LIT.Smabu.Domain.Shared.Common.Dtos
{
    public class CommunicationDto
    {
        public string Email { get; set; }
        public string Mobil { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public Communication ToValueObject()
        {
            return new Communication(Email, Mobil, Phone, Website);
        }

        internal static CommunicationDto Map(Communication communication)
        {
            return new CommunicationDto
            {
                Email = communication.Email,
                Mobil = communication.Mobil,
                Phone = communication.Phone,
                Website = communication.Website
            };
        }
    }
}
