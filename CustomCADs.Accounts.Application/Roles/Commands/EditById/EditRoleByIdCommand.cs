﻿using CustomCADs.Accounts.Application.Roles.Commands;

namespace CustomCADs.Accounts.Application.Roles.Commands.EditById;

public record EditRoleByIdCommand(RoleId Id, RoleWriteDto Dto) : ICommand;