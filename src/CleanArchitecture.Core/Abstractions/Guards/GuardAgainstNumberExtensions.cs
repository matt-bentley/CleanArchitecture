
namespace CleanArchitecture.Core.Abstractions.Guards
{
    public static partial class GuardClauseExtensions
    {
        public static void LessThan(this IGuardClause guardClause, decimal input, decimal minValue, string parameterName = "Value", string units = "", string? message = null)
        {
            if (input < minValue)
            {
                Error(message ?? $"'{parameterName}' must be greater than {minValue}{units}.");
            }
        }

        public static void ValueOutOfRange(this IGuardClause guardClause, decimal input, decimal minValue, decimal maxValue, string parameterName = "Value", string units = "", string? message = null)
        {
            if (input < minValue || input > maxValue)
            {
                Error(message ?? $"'{parameterName}' must be between {minValue}{units} and {maxValue}{units}.");
            }
        }
    }
}
