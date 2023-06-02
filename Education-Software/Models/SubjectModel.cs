using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("subjects")]

    public class SubjectModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(10)]
        [RegularExpression(@"^CS\d*$", ErrorMessage = "Id must start with 'CS'")]
        public string sub_id { get; set; }

        [Required]
        [MaxLength (50)]
        public string title { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        [RegularExpression(@"^[1-8]$", ErrorMessage = "The semester must be a number between 1 and 8.")]
        public int semester { get; set; }

        [Column(TypeName = "CHAR")]
        [Required]
        [RegularExpression(@"C|S|E")]
        public string sub_type { get; set; }

        [Column(TypeName = "TEXT")]
        [Required]
        public string description { get; set; }        

        [Column(TypeName = "TEXT")]
        [Required]
        public string learning_outcomes { get; set; }       

        [Column(TypeName = "TEXT")]
        [Required]
        public string skills_acquired { get; set; }

        
        [Column(TypeName = "TEXT")]
        [Required]
        public string specialization_link { get; set; }

        [Column(TypeName = "INT")]
        [Required]
        public int reading { get; set; }



        public ICollection<GradesModel> Grades { get; }
    }
}
