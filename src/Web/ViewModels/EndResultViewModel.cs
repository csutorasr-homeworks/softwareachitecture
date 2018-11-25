using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ScoreVievModel
    {

        public ScoreVievModel()
        {
            scores = new List<Score>();
        }

        public List<Score> scores;

        public class Score
        {
            string UserId { get; set; }
            int Points { get; set; }

        }

    }
}
