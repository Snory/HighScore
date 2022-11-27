using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Domain.Models
{
    public class LeaderBoardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
