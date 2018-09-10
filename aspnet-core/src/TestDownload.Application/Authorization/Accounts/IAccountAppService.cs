using System.Threading.Tasks;
using Abp.Application.Services;
using TestDownload.Authorization.Accounts.Dto;

namespace TestDownload.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
