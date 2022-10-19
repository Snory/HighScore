using System.ComponentModel.DataAnnotations;

namespace HighScore.API.Models
{
    public class HighScorePostPatchDTO
    {
        [Required]
        public float Score { get; set; }
    }
}
