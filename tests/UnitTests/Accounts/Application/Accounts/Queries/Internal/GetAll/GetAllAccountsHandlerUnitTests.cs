using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetAll;

using static Constants;
using static Constants.Users;

public class GetAllAccountsHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAllAccountsHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private readonly Account[] accounts = [
		Account.CreateWithId(AccountId.New(), Roles.Customer, CustomerUsername, CustomerEmail, DateTimeOffset.UtcNow),
		Account.CreateWithId(AccountId.New(), Roles.Contributor, ContributorUsername, ContributorEmail, DateTimeOffset.UtcNow),
		Account.CreateWithId(AccountId.New(), Roles.Designer, DesignerUsername, DesignerEmail, DateTimeOffset.UtcNow),
		Account.CreateWithId(AccountId.New(), Roles.Admin, AdminUsername, AdminEmail, DateTimeOffset.UtcNow),
	];
	private readonly AccountQuery query = new(Pagination: new(1, 1));

	public GetAllAccountsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(new Result<Account>(1, accounts));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllAccountsQuery query = new(this.query.Pagination);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllAccountsQuery query = new(this.query.Pagination);

		// Act
		Result<GetAllAccountsDto> accounts = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(accounts.Items.Select(r => r.Id), this.accounts.Select(r => r.Id)),
			() => Assert.Equal(accounts.Items.Select(r => r.Role), this.accounts.Select(r => r.RoleName)),
			() => Assert.Equal(accounts.Items.Select(r => r.Username), this.accounts.Select(r => r.Username)),
			() => Assert.Equal(accounts.Items.Select(r => r.Email), this.accounts.Select(r => r.Email))
		);
	}
}
