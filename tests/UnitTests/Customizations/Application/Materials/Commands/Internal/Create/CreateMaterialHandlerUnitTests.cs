using CustomCADs.Customizations.Application.Materials.Commands.Internal.Create;
using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Commands.Internal.Create;

using static MaterialsData;

public class CreateMaterialHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly CreateMaterialHandler handler;
	private readonly Mock<IWrites<Material>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Key = "generated-key";
	private const string ContentType = "content-type";
	private readonly Material material = CreateMaterialWithId();

	public CreateMaterialHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object, sender.Object);

		writes.Setup(x => x.AddAsync(It.Is<Material>(x => x.Id == material.Id), ct))
			.ReturnsAsync(CreateMaterialWithId(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureKey: Key,
			TextureContentType: ContentType
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<CreateImageCommand>(x => x.Key == Key && x.ContentType == ContentType),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureKey: Key,
			TextureContentType: ContentType
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(It.Is<Material>(x => x.Id == material.Id), ct), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureKey: Key,
			TextureContentType: ContentType
		);

		// Act
		MaterialId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureKey: Key,
			TextureContentType: ContentType
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.UpdateAsync(ValidId, It.Is<Material>(x => x.Id == material.Id)), Times.Once());
	}
}
