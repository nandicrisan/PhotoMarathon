using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Entities.Cms;

namespace PhotoMarathon.Data
{
    public class BaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        #region Entities

        DbSet<Newsletter> Newsletters { get; set; }
        DbSet<Photographer> Photographers { get; set; }
        DbSet<WorkShop> WorkShops { get; set; }
        DbSet<BlogItem> BlogItems { get; set; }
        DbSet<BillingData> BillingDatas { get; set; }
        DbSet<RegisterStatus> RegisterStatus { get; set; }
        DbSet<ContactMessage> ContactMessages { get; set; }

        //Cms
        DbSet<Page> Pages { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Article> Articles { get; set; }

        #endregion

        //Model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual void Commit()
        {
            this.SaveChanges();
        }
    }
}
