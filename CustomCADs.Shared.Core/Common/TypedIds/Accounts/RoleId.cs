using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Accounts;

public readonly struct RoleId(int value)
{
    public RoleId() : this(0) { }
    public int Value { get; init; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is RoleId roleId && this == roleId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(RoleId left, RoleId right)
        => left.Value == right.Value;

    public static bool operator !=(RoleId left, RoleId right)
        => !(left == right);
}
