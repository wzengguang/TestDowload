using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TestDownload.Controllers
{
    public abstract class TestDownloadControllerBase: AbpController
    {
        protected TestDownloadControllerBase()
        {
            LocalizationSourceName = TestDownloadConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
