using System.ComponentModel.DataAnnotations;

namespace HighScore.API.Models
{
    public class HighScoreWriteData
    {
        [Required]
        public float Score { get; set; }
    }
}
