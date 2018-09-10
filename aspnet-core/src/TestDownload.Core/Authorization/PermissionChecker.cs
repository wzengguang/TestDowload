using Abp.Authorization;
using TestDownload.Authorization.Roles;
using TestDownload.Authorization.Users;

namespace TestDownload.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
