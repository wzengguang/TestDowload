using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TestDownload.Configuration;
using TestDownload.Web;

namespace TestDownload.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TestDownloadDbContextFactory : IDesignTimeDbContextFactory<TestDownloadDbContext>
    {
        public TestDownloadDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TestDownloadDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TestDownloadDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TestDownloadConsts.ConnectionStringName));

            return new TestDownloadDbContext(builder.Options);
        }
    }
}
