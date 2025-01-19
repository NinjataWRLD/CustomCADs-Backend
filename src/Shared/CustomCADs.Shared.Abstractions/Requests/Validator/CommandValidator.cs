using CustomCADs.Shared.Abstractions.Requests.Commands;
using FluentValidation;

namespace CustomCADs.Shared.Abstractions.Requests.Validator;

public class CommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : ICommand;
public class CommandValidator<TCommand, TResponse> : AbstractValidator<TCommand> where TCommand : ICommand<TResponse>;
