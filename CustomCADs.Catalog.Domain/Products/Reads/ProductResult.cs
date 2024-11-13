using CustomCADs.Catalog.Domain.Products.Entities;

namespace CustomCADs.Catalog.Domain.Products.Reads;

public record ProductResult(int Count, ICollection<Product> Products);
