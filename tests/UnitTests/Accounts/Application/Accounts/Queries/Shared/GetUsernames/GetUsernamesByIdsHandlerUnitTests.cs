using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Username;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsernames;

using static AccountsData;
using static Constants.Roles;
using static Constants.Users;

public class GetUsernamesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetUsernamesByIdsHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly AccountId[] ids = [ValidId, ValidId, ValidId, ValidId];
	private static readonly string[] usernames = [CustomerUsername, ContributorUsername, DesignerUsername, AdminUsername];
	private static readonly AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);

	public GetUsernamesByIdsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(accountQuery, false, ct)).ReturnsAsync(new Result<Account>(
				Count: ids.Length,
				Items: [
					CreateAccountWithId(AccountId.New(), Customer, CustomerUsername),
					CreateAccountWithId(AccountId.New(), Contributor, ContributorUsername),
					CreateAccountWithId(AccountId.New(), Designer, DesignerUsername),
					CreateAccountWithId(AccountId.New(), Admin, AdminUsername),
				]
			));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetUsernamesByIdsQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(accountQuery, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetUsernamesByIdsQuery query = new(ids);

		// Act
		Dictionary<AccountId, string> result = await handler.Handle(query, ct);
		string[] actualUsernames = [.. result.Select(kvp => kvp.Value)];

		// Assert
		Assert.Equal(usernames, actualUsernames);
	}
}
