using PhotoMarathon.Data.Entities.Base;
using System.Collections.Generic;

namespace PhotoMarathon.Data.Entities
{
    public class WorkShop : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Photographer> Photographers { get; set; }
    }
}
