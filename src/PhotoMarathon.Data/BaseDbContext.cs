using Microsoft.EntityFrameworkCore;
using PhotoMarathon.Data.Entities;

namespace PhotoMarathon.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("PhotoMarathonBaseContext");
        }

        #region Entities
        DbSet<Newsletter> Newsletters { get; set; }
        #endregion  

        //Model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
