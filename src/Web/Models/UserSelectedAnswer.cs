using System;

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
    }
}