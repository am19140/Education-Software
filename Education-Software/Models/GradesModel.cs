using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("grades")]
    public class GradesModel
    {
        [Required]
        [Key]
        [MaxLength(6)]
        [RegularExpression(@"^p\d{5}$", ErrorMessage = "Invalid Username.")]
        public string username { get; set; } //takes the primary key of usermodel

        [Required]
        [Key]
        [ForeignKey("SubjectModel")]
        [MaxLength(10)]
        [RegularExpression(@"^CS\d*$", ErrorMessage = "Id must start with 'CS'")]
        public string sub_id { get; set; } //takes the primary key of subjectmodel

        [RegularExpression(@"^[0-10]$", ErrorMessage = "The grade must be a number between 0 and 10.")]
        public int? grade { get; set; }

    }
}
