using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Domain.TypedIds.Files;

public readonly struct ImageId
{
	public ImageId() : this(Guid.Empty) { }
	private ImageId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; }

	public static ImageId New() => new(Guid.NewGuid());
	public static ImageId New(Guid id) => new(id);
	public static Guid Unwrap(ImageId id) => id.Value;
	public static Guid? Unwrap(ImageId? id) => id?.Value;

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is ImageId imageId && this == imageId;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(ImageId left, ImageId right)
		=> left.Value == right.Value;

	public static bool operator !=(ImageId left, ImageId right)
		=> !(left == right);
}
