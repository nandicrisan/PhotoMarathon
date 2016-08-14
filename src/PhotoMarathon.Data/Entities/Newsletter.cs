using PhotoMarathon.Data.Entities.Base;
using System;

namespace PhotoMarathon.Data.Entities
{
    public class Newsletter : IEntityBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateAdded { get; set; } 
    }
}
