using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;
using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

public class GetCustomPaymentStatusesHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly GetCustomPaymentStatusesHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomPaymentStatusesQuery query = new();

		// Act
		string[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetNames<PaymentStatus>());
	}
}
