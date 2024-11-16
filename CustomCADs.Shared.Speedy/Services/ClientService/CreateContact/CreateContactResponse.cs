namespace CustomCADs.Shared.Speedy.Services.ClientService.CreateContact;

public record CreateContactResponse(
    long ClientId,
    ErrorDto? Error
);