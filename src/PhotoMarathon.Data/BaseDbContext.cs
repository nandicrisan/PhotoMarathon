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
            optionsBuilder.UseSqlServer("Data Source=SQL5024.SmarterASP.NET;Initial Catalog=DB_A0A4AE_photomarathon;User Id=DB_A0A4AE_photomarathon_admin;Password=Paradicsomle91;");
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
