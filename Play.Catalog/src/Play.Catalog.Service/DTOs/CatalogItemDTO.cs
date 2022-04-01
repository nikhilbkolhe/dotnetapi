using System;

namespace Play.Catalog.Service.DTOs
{
    public record CatalogItemDto ( Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreateDate);
}