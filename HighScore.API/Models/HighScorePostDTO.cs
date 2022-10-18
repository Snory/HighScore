using System.ComponentModel.DataAnnotations;

namespace HighScore.API.Models
{
    public class HighScorePostDTO
    {
        [Required]
        public float Score { get; set; }
    }
}
