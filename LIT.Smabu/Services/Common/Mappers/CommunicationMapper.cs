using LIT.Smabu.Business.Service.Contratcs;
using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Business.Service.Common.Mappers
{
    public class CommunicationMapper : IMapper<CommunicationDTO, Communication>, IMapper<Communication, CommunicationDTO>
    {
        public Communication Map(CommunicationDTO source)
        {
            return new Communication(source.Email, source.Mobil, source.Phone, source.Website);
        }

        public CommunicationDTO Map(Communication source)
        {
            return new CommunicationDTO()
            {
                Email = source.Email,
                Mobil = source.Mobil,
                Phone = source.Phone,
                Website = source.Website
            };
        }
    }
}
