using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.ShipmentId;

public class PurchasedCartSetShipmentIdData : TheoryData<Dictionary<ProductId, decimal>, Dictionary<ProductId, CadId>, Dictionary<CadId, CadId>>;
