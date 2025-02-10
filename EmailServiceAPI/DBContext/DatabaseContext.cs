using EmailServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailServiceAPI.DBContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<EmailLogModel> EmailLogs { get; set; }
        public DbSet<SMTPSettingModel> SmtpSettings { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<KPIModel> KPIs { get; set; }
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //modelBuilder.Entity<AreaRegisterMappingHeaderDto>().HasNoKey();

    //}

}
