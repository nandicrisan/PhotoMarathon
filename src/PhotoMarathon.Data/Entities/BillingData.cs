using PhotoMarathon.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoMarathon.Data.Entities
{
    public class BillingData : IEntityBase
    {
        [ForeignKey("Photographer")]
        public int Id { get; set; }

        public bool LegalPerson { get; set; }

        public string Address { get; set; }

        public string NrReg { get; set; }

        public string Cnp { get; set; }

        public string Bank { get; set; }

        public string Cont { get; set; }

        public virtual Photographer Photographer { get; set; }
    }
}
