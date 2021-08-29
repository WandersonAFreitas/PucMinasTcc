using ApplicationCore.Exceptions;
using ApplicationCore.Entities.Identity;

namespace Ardalis.GuardClauses
{
    public static class FooGuard
    {
        public static void NullUser(this IGuardClause guardClause, long userId, User user)
        {
            if (user == null)
                throw new UserNotFoundException(userId);
        }
    }
}