namespace CustomCADs.Account.Application.Users.Commands.DeleteById;

public record DeleteUserByIdCommand(UserId Id) : ICommand;