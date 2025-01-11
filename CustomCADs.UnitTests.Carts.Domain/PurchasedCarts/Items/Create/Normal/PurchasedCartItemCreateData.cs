using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Normal;

public class PurchasedCartItemCreateData : TheoryData<PurchasedCartId, ProductId, CadId, decimal, int, bool>;
