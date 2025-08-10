using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Queries.Internal.GetById;

using static TagsData;

public class GetTagByIdHandlerUnitTests : TagsBaseUnitTests
{
	private readonly GetTagByIdHandler handler;
	private readonly Mock<ITagReads> reads = new();

	private readonly static TagId id = ValidId;

	public GetTagByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateTag(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetTagByIdQuery query = new(id);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(id, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetTagByIdQuery query = new(id);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidId, result.Id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenTagNotFound()
	{
		// Arrange
		reads.Setup(v => v.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(null as Tag);

		GetTagByIdQuery query = new(id);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
