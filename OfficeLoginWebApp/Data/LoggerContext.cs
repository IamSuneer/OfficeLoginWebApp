using Microsoft.EntityFrameworkCore;
using OfficeLoginWebApp.Models;

namespace OfficeLoginWebApp.Data
{
    public class LoggerContext : DbContext
    {
        public LoggerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<OfficeLogger> officeLoggers { get; set; }

        public DbSet<UserSigninLog> userSigninLogs { get; set; }
    }
}
