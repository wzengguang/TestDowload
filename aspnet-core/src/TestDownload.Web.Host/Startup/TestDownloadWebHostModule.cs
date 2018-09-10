using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestDownload.Configuration;

namespace TestDownload.Web.Host.Startup
{
    [DependsOn(
       typeof(TestDownloadWebCoreModule))]
    public class TestDownloadWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TestDownloadWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDownloadWebHostModule).GetAssembly());
        }
    }
}
