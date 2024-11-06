namespace CustomCADs.Account.Application.Users.Commands.DeleteById;

public record DeleteUserByIdCommand(Guid Id) : ICommand;