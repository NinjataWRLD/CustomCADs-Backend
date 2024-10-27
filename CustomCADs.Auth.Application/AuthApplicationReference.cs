using System.Reflection;

namespace CustomCADs.Auth.Application;

public class AuthApplicationReference
{
    public static Assembly Assembly => typeof(AuthApplicationReference).Assembly;
}