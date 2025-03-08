using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.ForDelivery;

public class ActiveCartItemCreateData : TheoryData<ActiveCartId, ProductId, CustomizationId>;
