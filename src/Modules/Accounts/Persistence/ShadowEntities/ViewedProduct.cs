using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Accounts.Persistence.ShadowEntities;

public class ViewedProduct
{
	private ViewedProduct() { }

	public AccountId AccountId { get; set; }
	public ProductId ProductId { get; set; }
	public Account Account { get; set; } = null!;

	public static ViewedProduct Create(AccountId id, ProductId productId)
		=> new() { AccountId = id, ProductId = productId };
}
