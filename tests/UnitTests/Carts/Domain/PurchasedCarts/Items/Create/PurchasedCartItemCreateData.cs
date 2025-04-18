﻿using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create;

public class PurchasedCartItemCreateData : TheoryData<PurchasedCartId, ProductId, CadId, CustomizationId, decimal, int, bool>;
