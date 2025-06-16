using System.Reflection;

namespace CustomCADs.Tools.CodeGen;

public static class CodeGenReference
{
	public static Assembly Assembly => typeof(CodeGenReference).Assembly;
}
