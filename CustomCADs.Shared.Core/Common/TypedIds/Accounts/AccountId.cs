using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Accounts;

public readonly struct AccountId(Guid value)
{
    public AccountId() : this(Guid.Empty) { }
    public Guid Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is AccountId userId && this == userId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(AccountId left, AccountId right)
        => left.Value == right.Value;

    public static bool operator !=(AccountId left, AccountId right)
        => !(left == right);
}
