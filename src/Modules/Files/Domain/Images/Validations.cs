namespace CustomCADs.Files.Domain.Images;

public static class Validations
{
	public static Image ValidateKey(this Image image)
		=> image
			.ThrowIfNull(
				expression: x => x.Key,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Image ValidateContentType(this Image image)
		=> image
			.ThrowIfNull(
				expression: x => x.ContentType,
				predicate: string.IsNullOrWhiteSpace
			);
}
