using PhotoMarathon.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoMarathon.Data.Entities
{
    public class Newsletter : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage = "Adresa de e-mail nu este validă.")]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateAdded { get; set; } 
    }
}
