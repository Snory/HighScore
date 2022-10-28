using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Domain.Entities
{
    [Table("HighScores")]
    public class HighScoreEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public float Score { get; set; }
        [Required]
        public UserEntity User { get; set; } = null!;
        public int UserId { get; set; }
    }
}
