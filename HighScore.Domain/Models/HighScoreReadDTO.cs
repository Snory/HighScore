namespace HighScore.Domain.Models
{
    public class HighScoreReadDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int LeaderBoardId { get; set; }
        public float Score { get; set; }

    }
}
