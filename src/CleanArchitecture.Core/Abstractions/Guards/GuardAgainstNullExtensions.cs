
namespace CleanArchitecture.Core.Abstractions.Guards
{
    public static partial class GuardClauseExtensions
    {
        public static string NullOrEmpty(this IGuardClause guardClause, string input, string parameterName = "value", string? message = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                Error(message ?? $"Required input '{parameterName}' is missing.");
            }
            return input;
        }
    }
}
