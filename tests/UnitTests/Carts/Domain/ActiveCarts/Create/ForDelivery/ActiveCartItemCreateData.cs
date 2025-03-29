using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery;

public class ActiveCartItemCreateData : TheoryData<AccountId, ProductId, CustomizationId>;
