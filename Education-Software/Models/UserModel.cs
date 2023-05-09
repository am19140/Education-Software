using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Education_Software.Models
{
    public class UserModel
    {
        [Required]
        [Key]
        public string username { get; set; }

        [Required]
        [MaxLength(30)]
        public string pass { get; set; }

        [Required]
        [MaxLength(50)]
        public string first_name { get; set; }

        [Required]
        [MaxLength(50)] 
        public string last_name { get; set; }

        [Required]
        [MaxLength(1)]
        [RegularExpression(@"U|G", ErrorMessage = "Please enter 'U' or 'G'.")]
        public string student_state { get; set; }

        [RegularExpression(@"M|F|O", ErrorMessage = "Please enter 'M' , 'F' or 'O'.")]
        public string gender { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-z0-9._+%-]+@[A-Za-z0-9.-]+[.][A-Za-z]+$", ErrorMessage = "Invalid Phone Number.")]

        public string email { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string phone_number { get; set; }

    }
}
