using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TestDownload.Configuration.Dto;

namespace TestDownload.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TestDownloadAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
