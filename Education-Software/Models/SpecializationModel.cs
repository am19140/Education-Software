using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("specializations")]
    public class SpecializationModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(50)]
        public string spe_id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [MaxLength(50)]
        public string spe_name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [MaxLength(50)]
        [ForeignKey("SubjectModel")]
        public string sub_id { get; set; }

    }
}

