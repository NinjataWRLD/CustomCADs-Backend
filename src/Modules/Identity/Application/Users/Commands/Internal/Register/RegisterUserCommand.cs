﻿namespace CustomCADs.Identity.Application.Users.Commands.Internal.Register;

public record RegisterUserCommand(
    string Role,
    string Username,
    string Email,
    string Password,
    string TimeZone,
    string? FirstName,
    string? LastName
) : ICommand;
