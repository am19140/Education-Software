using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("questions")]
    public class QuestionModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(40)]
        public string q_id { get; set; }

        [Column(TypeName = "TEXT")]
        [Required]
        public string question { get; set; }

        [Column(TypeName = "TEXT")]
        [Required]
        public string answer { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [ForeignKey("SubjectModel")]
        [MaxLength(10)]
        [RegularExpression(@"^CS\d*$", ErrorMessage = "Id must start with 'CS'")]
        public string sub_id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [MaxLength(10)]
        [RegularExpression("^(description|learning_outcomes|skills_acquired|" +
            "specialization_link)$")]
        public string chapter { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [MaxLength(20)]
        [RegularExpression("^(multiple_choice|true/false|completion|matching|ordering)$")]
        public string q_type { get; set; }




    }
}
