﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education_Software.Models
{
    [Table("statistics")]
    public class StatisticsModel
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [Key]
        [MaxLength(6)]
        [RegularExpression(@"^p\d{5}$", ErrorMessage = "Invalid Username.")]
        [ForeignKey("UserModel")]
        public string username { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int description_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int learning_outcomes_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int skills_acquired_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int specialization_link_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int multiple_choice_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int true_false_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int completion_score { get; set; }

        [Column(TypeName = "NUMERIC")]
        [Required]
        public int matching_score { get; set; }
    }
}
