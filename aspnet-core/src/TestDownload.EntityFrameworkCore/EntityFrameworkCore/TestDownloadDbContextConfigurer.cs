using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TestDownload.EntityFrameworkCore
{
    public static class TestDownloadDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TestDownloadDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TestDownloadDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
