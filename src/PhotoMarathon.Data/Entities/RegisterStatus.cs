using PhotoMarathon.Data.Entities.Base;

namespace PhotoMarathon.Data.Entities
{
    public class RegisterStatus : IEntityBase
    {
        public int Id { get; set; }
        public bool Inactive { get; set; }
        public string Message { get; set; }
    }
}
