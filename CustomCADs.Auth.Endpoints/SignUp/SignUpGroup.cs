﻿namespace CustomCADs.Auth.Endpoints.SignUp;

public class SignUpGroup : Group
{
    public SignUpGroup()
    {
        Configure("auth/signup", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(d => d.WithTags("02. Sign-up Operations"));
        });
    }
}