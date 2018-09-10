using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TestDownload.Authorization.Roles;
using TestDownload.Authorization.Users;
using TestDownload.MultiTenancy;

namespace TestDownload.EntityFrameworkCore
{
    public class TestDownloadDbContext : AbpZeroDbContext<Tenant, Role, User, TestDownloadDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public TestDownloadDbContext(DbContextOptions<TestDownloadDbContext> options)
            : base(options)
        {
        }
    }
}
