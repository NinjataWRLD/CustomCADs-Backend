using Microsoft.AspNetCore.Http;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

public record PutProductRequest(Guid Id, string Name, string Description, int CategoryId, decimal Cost, IFormFile? Image = default);
