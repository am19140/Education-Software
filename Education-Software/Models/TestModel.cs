using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("tests")]
    public class TestModel
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
        [MaxLength(20)]
        public string test_id { get; set; }

        [Column(TypeName = "CHAR")]
        [Required]
        [MaxLength(1)]
        [RegularExpression(@"^[A,E]{1}$", ErrorMessage = "Invalid Test Type.")]
        public string test_type { get; set; }

        [Column(TypeName = "INT")]
        [Required]
        public int score { get; set; }

        [Column(TypeName = "INT")]
        [Required]
        public int trials { get; set; }

    }
}
