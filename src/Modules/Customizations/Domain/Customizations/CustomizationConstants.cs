using System.Text.RegularExpressions;

namespace CustomCADs.Customizations.Domain.Customizations.Validation;

public static partial class CustomizationConstants
{
	public const decimal ScaleMin = 1m;
	public const decimal ScaleMax = 10m;

	public const decimal InfillMin = 0.2m;
	public const decimal InfillMax = 1m;

	public const decimal VolumeMin = 0;
	public const decimal CostMin = 0m;

	/// <summary>
	///     In USD
	/// </summary>
	public const decimal ProfitMargin = 1.2m;
	public const decimal WallFactor = 0.45m;

	public static Regex Color => ColorRegex();

	[GeneratedRegex(@"^#[0-9a-fA-F]{6}$", RegexOptions.Compiled)]
	private static partial Regex ColorRegex();
}
