using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetExists;

public class GetCadExistsByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadExistsByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();

	public GetCadExistsByIdHandlerUnitTests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCadExistsByIdQuery query = new(id);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ExistsByIdAsync(id, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult_WhenProductExists()
	{
		// Arrange
		GetCadExistsByIdQuery query = new(id);

		// Act
		bool exists = await handler.Handle(query, ct);

		// Assert
		Assert.True(exists);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult_WhenProductDoesNotExists()
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(false);
		GetCadExistsByIdQuery query = new(id);

		// Act
		bool exists = await handler.Handle(query, ct);

		// Assert
		Assert.False(exists);
	}
}
