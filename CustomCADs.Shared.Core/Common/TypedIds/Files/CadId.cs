using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Core.Common.TypedIds.Files;

public readonly struct CadId(Guid value)
{
    public CadId() : this(Guid.Empty) { }

    public Guid Value { get; } = value;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CadId cadId && this == cadId;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();

    public static bool operator ==(CadId left, CadId right)
        => left.Value == right.Value;

    public static bool operator !=(CadId left, CadId right)
        => !(left == right);
}
