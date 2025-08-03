using CustomCADs.Idempotency.Application.IdempotencyKeys.BackgroundJobs;
using CustomCADs.Idempotency.Domain.Repositories;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.BackgroundJobs;

using static IdempotencyKeyConstants;

public class ClearIdempotencyKeysJobUnitTests : BaseIdempotencyKeyUnitTests
{
	private readonly ClearIdempotencyKeysJob job;
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly DateTimeOffset today = DateTimeOffset.UtcNow.Date;

	public ClearIdempotencyKeysJobUnitTests()
	{
		job = new(uow.Object);
	}

	[Fact]
	public async Task Execute_ShouldPersistToDatabase()
	{
		// Arrange
		Mock<Quartz.IJobExecutionContext> context = new();

		// Act
		await job.Execute(context.Object);

		// Assert
		uow.Verify(x => x.ClearIdempotencyKeysAsync(
			It.Is<DateTimeOffset>(x =>
				(today - x.Date).TotalHours == ClearIdempotencyKeysBeforeHours
				|| (today - x.Date.AddDays(-1)).TotalHours == ClearIdempotencyKeysBeforeHours // for flakiness
			),
			ct
		));
	}
}
