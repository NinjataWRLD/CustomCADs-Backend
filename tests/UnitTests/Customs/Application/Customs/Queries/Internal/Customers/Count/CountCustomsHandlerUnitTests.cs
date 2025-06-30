using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.Count;

using static CustomsData;

public class CountCustomsHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly CountCustomsHandler handler;
	private readonly Mock<ICustomReads> reads = new();

	private readonly static Dictionary<CustomStatus, int> expected = new()
	{
		[CustomStatus.Pending] = 1,
		[CustomStatus.Accepted] = 2,
		[CustomStatus.Begun] = 3,
		[CustomStatus.Finished] = 4,
		[CustomStatus.Completed] = 5,
		[CustomStatus.Reported] = 6,
	};

	public CountCustomsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountAsync(ValidBuyerId, ct))
			.ReturnsAsync(expected);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CountCustomsQuery query = new(ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountAsync(ValidBuyerId, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CountCustomsQuery query = new(ValidBuyerId);

		// Act
		CountCustomsDto counts = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(expected[CustomStatus.Pending], counts.Pending),
			() => Assert.Equal(expected[CustomStatus.Accepted], counts.Accepted),
			() => Assert.Equal(expected[CustomStatus.Begun], counts.Begun),
			() => Assert.Equal(expected[CustomStatus.Finished], counts.Finished),
			() => Assert.Equal(expected[CustomStatus.Completed], counts.Completed),
			() => Assert.Equal(expected[CustomStatus.Reported], counts.Reported)
		);
	}
}
