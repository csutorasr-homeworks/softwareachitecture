using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class UserSelectedAnswer
    {
        public Guid Id { get; set; }
        public Guid GameQuestionId { get; set; }
        public GameQuestion GameQuestion { get; set; }
        public Guid UserGameSessionId { get; set; }
        public UserGameSessions UserGameSession { get; set; }
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
        public DateTime AnswerTime { get; set; }
        [NotMapped]
        public int Points
        {
            get
            {
                if (Answer.IsCorrect)
                {
                    var time = (int)(AnswerTime - GameQuestion.StartTime.Value).TotalMilliseconds / 100;
                    if (time > 100)
                    {
                        return 0;
                    }
                    return 100 - time;
                }
                return -50;
            }
        }
    }
}