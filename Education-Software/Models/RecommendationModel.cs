﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("recommendations")]
    public class RecommendationModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(6)]
        [RegularExpression(@"^p\d{5}$", ErrorMessage = "Invalid Username.")]
        [ForeignKey("UserModel")]
        public string username { get; set; }

        [Column(TypeName = "TEXT")]
        [Required]
        public string recommendation { get; set; }

    }
}
