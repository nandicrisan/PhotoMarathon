namespace PhotoMarathon.Data.Infrastructure
{
    public class DbFactory : DbDisposable, IDbFactory
    {
        BaseDbContext dbContext;
        public BaseDbContext Init()
        {
            return dbContext ?? (dbContext = new BaseDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
