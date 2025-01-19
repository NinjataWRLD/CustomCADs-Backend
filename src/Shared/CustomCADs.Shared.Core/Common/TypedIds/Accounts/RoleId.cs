using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Accounts;

public readonly struct RoleId
{
    public RoleId() : this(0) { }
    private RoleId(int value)
    {
        Value = value;
    }

    public int Value { get; init; }

    public static RoleId New() => new(0);
    public static RoleId New(int id) => new(id);

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
