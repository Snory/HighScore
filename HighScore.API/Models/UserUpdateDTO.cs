﻿using System.ComponentModel.DataAnnotations;

namespace HighScore.API.Models
{
    public class UserUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}