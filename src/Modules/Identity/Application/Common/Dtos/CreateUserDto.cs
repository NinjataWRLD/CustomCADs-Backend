﻿namespace CustomCADs.Identity.Application.Common.Dtos;

public record CreateUserDto(
    string Role,
    string Username,
    string Email,
    string Password
);
