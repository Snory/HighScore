using System.ComponentModel.DataAnnotations;
namespace HighScore.Domain.Models
{
    public class UserWriteDTO
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
    }
}
