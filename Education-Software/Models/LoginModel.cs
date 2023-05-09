using System.ComponentModel.DataAnnotations;

namespace Software_Engineering_Project.Models
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
