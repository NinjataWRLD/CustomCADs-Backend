﻿using CustomCADs.Accounts.Application.Roles.Commands;

namespace CustomCADs.Accounts.Application.Roles.Commands.EditByName;

public record EditRoleByNameCommand(string Name, RoleWriteDto Dto) : ICommand;