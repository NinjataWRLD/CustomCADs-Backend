using CustomCADs.Printing.Application.Customizations.Queries.Shared.Exists;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Exists;

using static CustomizationsData;

public class GetCustomizationExistsByIdHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationExistsByIdHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();

	public GetCustomizationExistsByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct))
			.ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCustomizationExistsByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ExistsByIdAsync(ValidId, ct), Times.Once());
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct))
			.ReturnsAsync(exists);
		GetCustomizationExistsByIdQuery query = new(ValidId);

		// Act
		bool result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
