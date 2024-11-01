using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs
{
    public record CatalogGroupDTO(CatalogGroupId Id, string Name, CatalogItemDTO[] Items) : IDTO
    {
        public string DisplayName => Name;

        internal static CatalogGroupDTO Create(CatalogGroup catalogGroup)
        {
            var items = catalogGroup.Items.Select(x => CatalogItemDTO.Create(x)).ToArray();
            return new CatalogGroupDTO(catalogGroup.Id, catalogGroup.Name, items);
        }
    }
}