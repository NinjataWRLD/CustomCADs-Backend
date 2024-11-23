namespace CustomCADs.Inventory.Domain.Products.Reads;

public record ProductResult(int Count, ICollection<Product> Products);
