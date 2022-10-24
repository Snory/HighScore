using System.ComponentModel.DataAnnotations;
namespace HighScore.Domain.Models
{
    public class UserWriteData
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
