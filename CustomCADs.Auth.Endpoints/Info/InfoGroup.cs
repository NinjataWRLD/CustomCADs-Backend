﻿using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Auth.Endpoints.Info;

public class InfoGroup : Group
{
    public InfoGroup()
    {
        Configure("auth/info", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(opt =>
            {
                opt.WithTags("Auth_Info");
            });
        });
    }
}