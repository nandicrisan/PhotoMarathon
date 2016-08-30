using Microsoft.EntityFrameworkCore;
using PhotoMarathon.Data.Entities;

namespace PhotoMarathon.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        #region Entities
        DbSet<Newsletter> Newsletters { get; set; }
        DbSet<Photographer> Photographers { get; set; }
        DbSet<WorkShop> WorkShops { get; set; }
        #endregion

        //Model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual void Commit()
        {
            // var res = base.SaveChanges();
            this.SaveChanges();
        }
    }
}
