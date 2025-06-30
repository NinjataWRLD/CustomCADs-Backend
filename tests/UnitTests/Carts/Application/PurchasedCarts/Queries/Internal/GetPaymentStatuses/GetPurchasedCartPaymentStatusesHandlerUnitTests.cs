using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

public class GetPurchasedCartPaymentStatusesHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly GetPurchasedCartPaymentStatusesHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetPurchasedCartPaymentStatusesQuery query = new();

		// Act
		string[] statuses = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Enum.GetNames<PaymentStatus>(), statuses);
	}
}
