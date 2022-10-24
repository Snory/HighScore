using System.ComponentModel.DataAnnotations;

namespace HighScore.Domain.Models
{
    public class HighScoreWriteDataDTO
    {
        [Required]
        public float Score { get; set; }
    }
}
