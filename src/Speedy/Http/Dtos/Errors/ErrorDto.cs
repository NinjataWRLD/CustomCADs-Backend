namespace CustomCADs.Speedy.Http.Dtos.Errors;

/// <param name="Id">
///     Error id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Code">
///     Error code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Message">
///     Error message.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Context">
///     Error context, if relevant.
///     <br />
///     Required: No.
/// </param>
/// <param name="Component">
///     Error component, if relevant.
///     <br />
///     Required: No.
/// </param>
internal record ErrorDto(
	string Id,
	int Code,
	string Message,
	string? Context,
	string? Component
);
