using System.ComponentModel.DataAnnotations;

namespace HighScore.Domain.Models
{
    public class HighScoreWriteDataDTO
    {
        [Required]
        public float Score { get; set; }

        public int UserId { get; set; }

        public int LeaderBoardId { get; set; }
    }
}
