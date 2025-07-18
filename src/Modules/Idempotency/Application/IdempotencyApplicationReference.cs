using System.Reflection;

namespace CustomCADs.Idempotency.Application;

public class IdempotencyApplicationReference
{
	public static Assembly Assembly => typeof(IdempotencyApplicationReference).Assembly;
}
