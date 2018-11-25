using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class GameQuestion
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public GameSession Game { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTime? StartTime { get; set; }
        public ICollection<UserSelectedAnswer> UserSelectedAnswers { get; set; }
    }
}