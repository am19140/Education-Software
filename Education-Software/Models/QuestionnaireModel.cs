using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("questionnaire")]
    public class QuestionnaireModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(50)]
        public string q_id { get; set; }

        [Column(TypeName = "TEXT")]
        [Required]
        public string question { get; set; }


    }
}
