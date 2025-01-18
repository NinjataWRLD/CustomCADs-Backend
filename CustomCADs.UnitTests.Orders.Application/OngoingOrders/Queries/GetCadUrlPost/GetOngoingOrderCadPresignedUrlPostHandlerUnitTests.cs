using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Application.OngoingOrders.Queries.GetCadUrlPost;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.GetCadUrlPost;

using static OngoingOrdersData;

public class GetOngoingOrderCadPresignedUrlPostHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string ContentType = "model/gltf-binary";
    private const string FileName = "Model.glb";
    private const string GeneratedKey = "generated-key";
    private const string PresignedUrl = "presigned.url";
    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId designerId = AccountId.New();
    private static readonly OngoingOrder order = CreateOrder()
        .SetDesignerId(designerId);

    public GetOngoingOrderCadPresignedUrlPostHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlPostByIdQuery>(), ct))
            .ReturnsAsync((GeneratedKey, PresignedUrl));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetOngoingOrderCadPresignedUrlPostQuery query = new(
            Id: id,
            ContentType: ContentType,
            FileName: FileName,
            DesignerId: designerId
        );
        GetOngoingOrderCadPresignedUrlPostHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetOngoingOrderCadPresignedUrlPostQuery query = new(
            Id: id,
            ContentType: ContentType,
            FileName: FileName,
            DesignerId: designerId
        );
        GetOngoingOrderCadPresignedUrlPostHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCadPresignedUrlPostByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnsProperly()
    {
        // Arrange
        GetOngoingOrderCadPresignedUrlPostQuery query = new(
            Id: id,
            ContentType: ContentType,
            FileName: FileName,
            DesignerId: designerId
        );
        GetOngoingOrderCadPresignedUrlPostHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(GeneratedKey, result.GeneratedKey),
            () => Assert.Equal(PresignedUrl, result.PresignedUrl)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowsException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as OngoingOrder);

        GetOngoingOrderCadPresignedUrlPostQuery query = new(
            Id: id,
            ContentType: ContentType,
            FileName: FileName,
            DesignerId: designerId
        );
        GetOngoingOrderCadPresignedUrlPostHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
