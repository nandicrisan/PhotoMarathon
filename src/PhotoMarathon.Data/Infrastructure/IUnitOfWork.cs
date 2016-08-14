namespace PhotoMarathon.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
