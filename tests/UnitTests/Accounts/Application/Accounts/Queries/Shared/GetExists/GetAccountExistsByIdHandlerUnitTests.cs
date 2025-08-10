using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Exists;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists;

using static AccountsData;

public class GetAccountExistsByIdHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountExistsByIdHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	public GetAccountExistsByIdHandlerUnitTests()
	{
		handler = new(reads.Object);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAccountExistsByIdQuery query = new(ValidId);

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
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(exists);
		GetAccountExistsByIdQuery query = new(ValidId);

		// Act
		bool result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
