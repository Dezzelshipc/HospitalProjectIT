using Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(model => model.UserName);
            modelBuilder.Entity<UserModel>().HasKey(model => model.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
