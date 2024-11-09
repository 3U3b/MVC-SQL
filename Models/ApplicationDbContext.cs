using Microsoft.EntityFrameworkCore;
namespace MVCwithSQLITE.Models
{
        public class ApplicationDbContext : DbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 使用 Fluent API 指定主鍵
            modelBuilder.Entity<Leader>().HasKey(p => p.LeaderId);
        }*/
        public DbSet<Leader> Leader { get; set; } // 班代的表
        }
    }
