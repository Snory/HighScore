﻿namespace HighScore.Domain.Models
{
    public class HighScoreDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int LeaderBoardId { get; set; }
        public float Score { get; set; }

    }
}
