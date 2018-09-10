using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestDownload.Authorization;

namespace TestDownload
{
    [DependsOn(
        typeof(TestDownloadCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TestDownloadApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TestDownloadAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TestDownloadApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
