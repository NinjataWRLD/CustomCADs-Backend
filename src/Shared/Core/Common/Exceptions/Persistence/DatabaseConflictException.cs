using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Persistence;

public class DatabaseConflictException : BaseException
{
    private DatabaseConflictException(string message, Exception? inner) : base(message, inner) { }

    public static DatabaseConflictException General(Exception? inner = null)
        => new("The Database has been concurrently updated such that a concurrency token that was expected to match did not actually match.", inner);

    public static DatabaseConflictException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
