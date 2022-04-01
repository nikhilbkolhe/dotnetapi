using System;

namespace Play.Catalog.Service.DTOs
{
    public record UpdateCatalogItemsDTO(string Name, string Description, decimal Price, DateTimeOffset LastUpdateDate);
}