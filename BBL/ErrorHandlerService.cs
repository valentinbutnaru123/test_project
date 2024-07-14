using System.Text;
using System.Linq;

public static class ErrorHandlerService
{
    private const int FirstValue = 1;

    public static string ParameterNotValidError(string parameter)
    {
        return $"{parameter} is not valid";
    }
	public static string ParameterNotValidError(int parameter)
	{
		return $"{parameter} is not valid";
	}

	public static string ParameterAlreadyExistsError(string parameter)
    {
        return $"{parameter} already exists";
    }

    public static string ParameterNotFoundError(string parameter)
    {
        return $"{parameter} not found";
    }

    public static string ParameterLessThanError(string parameter, int value)
    {
        return $"{parameter} is less than {value}";
    }

    public static string ParameterGreaterThanError(string parameter, int value)
    {
        return $"{parameter} is greater than {value}";
    }

    public static string ParameterMustHaveError(string parameter, string[] values)
    {
        var str = new StringBuilder($"{parameter} must have only");

        var firstValue = values.FirstOrDefault();

        str.Append($" {firstValue}");

        foreach (var value in values.Skip(FirstValue))
        {
            str.Append($" and {value}");
        }

        return str.ToString();
    }
}