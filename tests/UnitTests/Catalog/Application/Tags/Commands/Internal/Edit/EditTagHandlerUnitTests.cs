using CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit.Data;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;

public class EditTagHandlerUnitTests : TagsBaseUnitTests
{
	private readonly Mock<ITagReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly TagId id = new();
	private static readonly Tag tag = CreateTag();

	public EditTagHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(tag);
	}

	[Theory]
	[ClassData(typeof(EditTagValidData))]
	public async Task Handler_ShouldQueryDatabase(string name)
	{
		// Arrange
		EditTagCommand command = new(id, name);
		EditTagHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(id, true, ct), Times.Once());
	}

	[Theory]
	[ClassData(typeof(EditTagValidData))]
	public async Task Handler_ShouldPersistToDatabase(string name)
	{
		// Arrange
		EditTagCommand command = new(id, name);
		EditTagHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(id, true, ct), Times.Once());
		uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
	}


	[Theory]
	[ClassData(typeof(EditTagValidData))]
	public async Task Handler_ShouldThrowException_WhenTagDoesNotExist(string name)
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Tag);

		EditTagCommand command = new(id, name);
		EditTagHandler handler = new(reads.Object, uow.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Tag>>(async () =>
		{
			// Act  
			await handler.Handle(command, ct);
		});
	}
}
