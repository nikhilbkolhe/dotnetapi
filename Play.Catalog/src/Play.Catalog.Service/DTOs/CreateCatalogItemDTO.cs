using System;

namespace Play.Catalog.Service.DTOs
{
    public record CreateCatalogItemDTO(string Name, string Description, decimal Price);
}