using System.ComponentModel.DataAnnotations;

namespace Education_Software.Models
{
    public class RegistrationModel
    {
        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        [MaxLength(1)]
        [RegularExpression(@"U|G", ErrorMessage = "Please enter 'U' or 'G'.")]
        public string student_state { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Insert a valid email address")]

        public string email { get; set; }

        [Required]
        public string gender { get; set; }


        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string phone_number { get; set; }
    }
}
