﻿namespace CustomCADs.Account.Application.Users.Queries.GetById;

public class GetUserByIdDto
{
    public required string Role { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
