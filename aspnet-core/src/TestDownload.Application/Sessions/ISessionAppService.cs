using System.Threading.Tasks;
using Abp.Application.Services;
using TestDownload.Sessions.Dto;

namespace TestDownload.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
