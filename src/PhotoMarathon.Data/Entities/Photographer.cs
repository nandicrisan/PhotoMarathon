using PhotoMarathon.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoMarathon.Data.Entities
{
    public class Photographer : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Te rugăm să completezi numele.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Te rugăm să completezi prenumele.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Adresa de e-mail nu este validă.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Numărul de telefon nu este validă.")]
        public string PhoneNumber { get; set; }

        public bool IsProfessionist { get; set; }

        public bool RegisterForWorkShop { get; set; }

        public bool RegisterForMarathon { get; set; }

        public virtual WorkShop Workshop { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
