using System.Threading.Tasks;
using TestDownload.Configuration.Dto;

namespace TestDownload.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
