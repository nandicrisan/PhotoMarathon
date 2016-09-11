using System.ComponentModel.DataAnnotations;

namespace PhotoMarathon.Models
{
    public class AccountViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
