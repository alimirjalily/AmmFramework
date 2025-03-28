using AmmFramework.Utilities.Guards;

namespace AmmFramework.Utilities.Guards.GuardClauses;

public static class NotNullGuardClause
{
    public static void Null<T>(this Guard guard, T value, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value == null)
            throw new InvalidOperationException(message);
    }
}