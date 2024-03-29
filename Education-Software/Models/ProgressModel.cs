﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging.Signing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("progress")]
    public class ProgressModel
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
        [ForeignKey("TestModel")]
        public string test_id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(10)]
        [RegularExpression(@"^CS\d*$", ErrorMessage = "Id must start with 'CS'")]
        [ForeignKey("SubjectModel")]
        public string sub_id { get; set; }

        [Column(TypeName = "CHAR")]
        [Required]
        [MaxLength(1)]
        [RegularExpression(@"^[A,E]{1}$", ErrorMessage = "Invalid Test Type.")]
        public string test_type { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int score { get; set; }

        [Column(TypeName = "TEXT")]
        [Required]
        public string time { get; set; }
    }
}
