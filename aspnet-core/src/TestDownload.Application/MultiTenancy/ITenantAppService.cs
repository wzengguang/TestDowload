using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDownload.MultiTenancy.Dto;

namespace TestDownload.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
