using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("grades")]
    public class GradesModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(6)]
        [RegularExpression(@"^p\d{5}$", ErrorMessage = "Invalid Username.")]
        [ForeignKey("UserModel")]
        public string username { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [ForeignKey("SubjectModel")]
        [MaxLength(10)]
        [RegularExpression(@"^CS\d*$", ErrorMessage = "Id must start with 'CS'")]
        public string sub_id { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        [RegularExpression(@"^[1-8]$", ErrorMessage = "The semester must be a number between 1 and 8.")]
        public string grade { get; set; }

    }
}
