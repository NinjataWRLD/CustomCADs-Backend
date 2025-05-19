using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Exists;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists;

public class GetAccountExistsByIdHandlerUnitTestsUnitTests : AccountsBaseUnitTests
{
	private readonly Mock<IAccountReads> reads = new();

	[Theory]
	[ClassData(typeof(GetAccountExistsByIdValidData))]
	public async Task Handle_ShouldQueryDatabase(AccountId id)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);

		GetAccountExistsByIdQuery query = new(id);
		GetAccountExistsByIdHandler handler = new(reads.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ExistsByIdAsync(id, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(GetAccountExistsByIdValidData))]
	public async Task Handle_ShouldReturnProperly_WhenAccountExists(AccountId id)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);

		GetAccountExistsByIdQuery query = new(id);
		GetAccountExistsByIdHandler handler = new(reads.Object);

		// Act
		bool exists = await handler.Handle(query, ct);

		// Assert
		Assert.True(exists);
	}

	[Theory]
	[ClassData(typeof(GetAccountExistsByIdValidData))]
	public async Task Handle_ShouldReturnProperly_WhenAccountDoesNotExists(AccountId id)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(false);

		GetAccountExistsByIdQuery query = new(id);
		GetAccountExistsByIdHandler handler = new(reads.Object);

		// Act
		bool exists = await handler.Handle(query, ct);

		// Assert
		Assert.False(exists);
	}
}
