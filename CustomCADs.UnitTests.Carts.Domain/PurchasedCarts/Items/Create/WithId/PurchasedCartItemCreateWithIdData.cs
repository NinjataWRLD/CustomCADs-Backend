using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.WithId;

public class PurchasedCartItemCreateWithIdData : TheoryData<PurchasedCartItemId, PurchasedCartId, ProductId, CadId, decimal, int, bool>;
