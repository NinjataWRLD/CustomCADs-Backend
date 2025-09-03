using System.Text.RegularExpressions;

namespace CustomCADs.Printing.Domain.Customizations;

public static partial class CustomizationConstants
{
	public const decimal ScaleMin = 1m;
	public const decimal ScaleMax = 10m;

	public const decimal InfillMin = 0.2m;
	public const decimal InfillMax = 1m;

	public const decimal VolumeMin = 0;
	public const decimal VolumeMax = 1_000_000_000;

	public static Regex Color => ColorRegex();

	[GeneratedRegex(@"^#[0-9a-fA-F]{6}$", RegexOptions.Compiled)]
	private static partial Regex ColorRegex();
}
