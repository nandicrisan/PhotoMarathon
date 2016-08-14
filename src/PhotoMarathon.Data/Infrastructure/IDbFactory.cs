namespace PhotoMarathon.Data.Infrastructure
{
    public interface IDbFactory
    {
        BaseDbContext Init();
    }
}
