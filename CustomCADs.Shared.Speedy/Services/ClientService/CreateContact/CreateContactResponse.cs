namespace CustomCADs.Shared.Speedy.Services.ClientService.CreateContact;

using Dtos.Errors;

public record CreateContactResponse(
    long ClientId,
    ErrorDto? Error
);