using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Add;

public class AddActiveCartData : TheoryData<AccountId, CustomizationId?, bool, ProductId>;
