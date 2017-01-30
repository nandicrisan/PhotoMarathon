using PhotoMarathon.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoMarathon.Data.Entities
{
    public class ContactMessage : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
