using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Queries.Internal.GetAll;

using static TagsData;

public class GetAllTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly GetAllTagsHandler handler;
	private readonly Mock<ITagReads> reads = new();

	private readonly Tag[] tags = [
		Tag.CreateWithId(ValidId, MinValidName),
		Tag.CreateWithId(ValidId, MaxValidName)
	];

	public GetAllTagHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(tags);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllTagsQuery query = new();

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.AllAsync(false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllTagsQuery query = new();

		// Act
		GetAllTagsDto[] tags = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(tags.Select(r => r.Id), this.tags.Select(r => r.Id)),
			() => Assert.Equal(tags.Select(r => r.Name), this.tags.Select(r => r.Name))
		);
	}
}
