using PhotoMarathon.Data.Entities;
using System.Collections.Generic;

namespace PhotoMarathon.Models
{
    public class RegisterViewModel
    {
        public Photographer Photographer { get; set; }
        public RegisterStatus RegisterStatus { get; set; }
        public List<WorkShop> Workshops { get; set; }
    }
}
