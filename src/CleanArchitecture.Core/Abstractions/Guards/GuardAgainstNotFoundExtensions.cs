
namespace CleanArchitecture.Core.Abstractions.Guards
{
    public static partial class GuardClauseExtensions
    {
        public static T NotFound<T>(this IGuardClause guardClause, T? aggregate, string? message = null) where T : class
        {
            if (aggregate == null)
            {
                NotFound(message ?? "Not found");
            }
            return aggregate!;
        }
    }
}
