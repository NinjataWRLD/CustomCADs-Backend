using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

using static Constants.Users;
using static CustomsData;

public class GetAllCustomsUnitTests : CustomsBaseUnitTests
{
	private readonly GetAllCustomsHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Custom[] customs = [
		CreateCustomWithId(id: ValidId),
		CreateCustomWithId(id: ValidId),
	];
	private readonly Dictionary<AccountId, string> customers = new()
	{
		[ValidBuyerId] = CustomerUsername,
	};
	private readonly Dictionary<AccountId, string> designers = new()
	{
		[ValidDesignerId] = DesignerUsername,
	};
	private readonly CustomQuery query;

	public GetAllCustomsUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		query = new(
			Pagination: new(1, customs.Length)
		);

		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(new Result<Custom>(
				customs.Length,
				customs
			));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernamesByIdsQuery>(x => x.Ids.Contains(ValidBuyerId)),
			ct
		)).ReturnsAsync(customers);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernamesByIdsQuery>(x => x.Ids.Contains(ValidDesignerId)),
			ct
		)).ReturnsAsync(designers);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllCustomsQuery query = new(this.query.Pagination);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllCustomsQuery query = new(this.query.Pagination);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		int expectedCount = customs.Length, actualCount = result.Count;
		CustomId[] expectedIds = [.. customs.Select(x => x.Id)],
			actualIds = [.. result.Items.Select(x => x.Id)];

		Assert.Multiple(
			() => Assert.Equal(expectedCount, actualCount),
			() => Assert.Equal(expectedIds, actualIds)
		);
	}
}
