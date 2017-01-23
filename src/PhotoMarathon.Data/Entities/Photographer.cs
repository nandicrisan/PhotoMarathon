using PhotoMarathon.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoMarathon.Data.Entities
{
    public class Photographer : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Te rugăm să completezi numele.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Te rugăm să completezi prenumele.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Te rugăm să completezi addressa de email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Adresa de e-mail nu este validă.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Te rugăm să completezi numărul de telefon")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Numărul de telefon nu este validă.")]
        public string PhoneNumber { get; set; }

        public bool IsProfessionist { get; set; }

        public bool RegisterForWorkShop { get; set; }

        public bool RegisterForMarathon { get; set; }

        public bool Rules { get; set; }

        public bool HasNewsLetter { get; set; }

        public int? WorkshopId { get; set; }
        public virtual WorkShop Workshop { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual BillingData BillingData { get; set; }

        public bool Active { get; set; }
        public string InactiveMessage { get; set; }
    }
}
