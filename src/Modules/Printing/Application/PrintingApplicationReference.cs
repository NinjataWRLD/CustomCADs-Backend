using System.Reflection;

namespace CustomCADs.Printing.Application;

public class PrintingApplicationReference
{
	public static Assembly Assembly => typeof(PrintingApplicationReference).Assembly;
}
