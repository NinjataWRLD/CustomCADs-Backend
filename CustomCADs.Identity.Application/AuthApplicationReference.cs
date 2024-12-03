using System.Reflection;

namespace CustomCADs.Identity.Application;

public class AuthApplicationReference
{
    public static Assembly Assembly => typeof(AuthApplicationReference).Assembly;
}