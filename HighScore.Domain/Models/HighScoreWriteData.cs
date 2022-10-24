using System.ComponentModel.DataAnnotations;

namespace HighScore.Domain.Models
{
    public class HighScoreWriteData
    {
        [Required]
        public float Score { get; set; }
    }
}
