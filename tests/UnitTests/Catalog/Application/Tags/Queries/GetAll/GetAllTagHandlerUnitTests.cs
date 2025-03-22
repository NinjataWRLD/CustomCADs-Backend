using CustomCADs.Catalog.Application.Tags.Queries.GetAll;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Queries.GetAll;

using static TagsData;

public class GetAllTagHandlerUnitTests : TagsBaseUnitTests
{
    private readonly Mock<ITagReads> reads = new();
    private readonly Tag[] tags = [
        Tag.CreateWithId(ValidId, ValidName1),
        Tag.CreateWithId(ValidId, ValidName2)
    ];

    public GetAllTagHandlerUnitTests()
    {
        reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(tags);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Assert
        GetAllTagsQuery query = new();
        GetAllTagsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(v => v.AllAsync(false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Assert
        GetAllTagsQuery query = new();
        GetAllTagsHandler handler = new(reads.Object);

        // Act
        GetAllTagsDto[] tags = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(tags.Select(r => r.Id), this.tags.Select(r => r.Id)),
            () => Assert.Equal(tags.Select(r => r.Name), this.tags.Select(r => r.Name))
        );
    }
}
