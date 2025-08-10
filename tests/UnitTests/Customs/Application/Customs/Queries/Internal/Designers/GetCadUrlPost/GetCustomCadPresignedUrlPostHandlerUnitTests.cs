using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Designers.GetCadUrlPost;

using static CustomsData;

public class GetCustomCadPresignedUrlPostHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly GetCustomCadPresignedUrlPostHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Name = "custom-name";
	private static readonly UploadFileRequest req = new("content-type", "file-name");
	private static readonly UploadFileResponse res = new("generated-key", "presigned-url");
	private readonly Custom custom = CreateCustom(name: Name);

	public GetCustomCadPresignedUrlPostHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		custom.Accept(ValidDesignerId);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlPostByIdQuery>(x => x.Name == Name && x.File == req),
			ct
		)).ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: ValidDesignerId,
			Cad: req
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlPostByIdQuery>(x => x.Name == Name && x.File == req),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: ValidDesignerId,
			Cad: req
		);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Custom);
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: ValidDesignerId,
			Cad: req
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorized()
	{
		// Arrange
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: AccountId.New(),
			Cad: req
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotAccepted()
	{
		// Arrange
		custom.Cancel();
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: ValidDesignerId,
			Cad: req
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
