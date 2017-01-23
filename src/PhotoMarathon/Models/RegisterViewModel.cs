using PhotoMarathon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMarathon.Models
{
    public class RegisterViewModel
    {
        public Photographer Photographer { get; set; }
        public RegisterStatus RegisterStatus { get; set; }
    }
}
