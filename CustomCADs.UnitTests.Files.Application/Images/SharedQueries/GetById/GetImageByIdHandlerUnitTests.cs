using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetById;

public class GetImageByIdHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();

    public GetImageByIdHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, track: false, ct).Returns(CreateImage());
    }

    [Fact]
    public async Task Handle_ShouldCallDatabase()
    {
        // Assert
        GetImageByIdQuery query = new(id);
        GetImageByIdHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, track: false, ct);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss()
    {
        // Assert
        reads.SingleByIdAsync(id, false, ct).Returns(null as Image);

        GetImageByIdQuery query = new(id);
        GetImageByIdHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
