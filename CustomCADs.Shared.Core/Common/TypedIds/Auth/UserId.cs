using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Auth;

public readonly struct UserId(Guid value)
{
    public UserId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is UserId userId && this == userId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(UserId left, UserId right)
        => left.Value == right.Value;

    public static bool operator !=(UserId left, UserId right)
        => !(left == right);
}
