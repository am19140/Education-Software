using System.ComponentModel.DataAnnotations;

namespace Education_Software.Models
{
    public class LoginModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public bool IsLoginConfirmed { get; set; } = true;
    }
}
