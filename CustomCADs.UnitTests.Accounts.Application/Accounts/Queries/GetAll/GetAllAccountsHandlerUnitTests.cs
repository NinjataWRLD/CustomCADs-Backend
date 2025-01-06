using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetAll;

using static Constants.Users;

public class GetAllAccountsHandlerUnitTests : AccountsBaseUnitTests
{
    private const int count = 30;

    private readonly Mock<IAccountReads> reads = new();
    private readonly Account[] accounts = Account.CreateRange([
        (Guid.NewGuid(), Constants.Roles.Client, ClientUsername, ClientEmail),
        (Guid.NewGuid(), Constants.Roles.Contributor, ContributorUsername, ContributorEmail),
        (Guid.NewGuid(), Constants.Roles.Designer, DesignerUsername, DesignerEmail),
        (Guid.NewGuid(), Constants.Roles.Admin, AdminUsername, AdminEmail),
    ]).ToArray();
    private readonly AccountQuery accountQuery = new(GetPagination());

    public GetAllAccountsHandlerUnitTests()
    {
        reads.Setup(x => x.AllAsync(accountQuery, false, ct))
            .ReturnsAsync(new Result<Account>(count, accounts));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAllAccountsQuery query = new(GetPagination());
        GetAllAccountsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(accountQuery, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetAllAccountsQuery query = new(GetPagination());
        GetAllAccountsHandler handler = new(reads.Object);

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
 
    private static Pagination GetPagination(int count = count)
            => new(1, count);
}
