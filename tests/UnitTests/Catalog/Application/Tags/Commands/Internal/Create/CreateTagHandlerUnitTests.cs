using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create;

using static TagsData;

public class CreateTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly CreateTagHandler handler;
	private readonly Mock<ITagWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<TagId, Tag>> cache = new();

	private readonly Tag tag = CreateTag();

	public CreateTagHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object);

		writes.Setup(v => v.AddAsync(
			It.Is<Tag>(x => x.Name == MaxValidName),
			ct
		)).ReturnsAsync(tag);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateTagCommand command = new(MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(v => v.AddAsync(
			It.Is<Tag>(x => x.Name == MaxValidName),
			ct
		), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		CreateTagCommand command = new(MaxValidName);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, tag),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateTagCommand command = new(MaxValidName);

		// Act
		TagId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
