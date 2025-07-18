using System.Reflection;

namespace CustomCADs.Idempotency.Persistence;

public class IdempotencyPersistenceReference
{
	public static Assembly Assembly => typeof(IdempotencyPersistenceReference).Assembly;
}
