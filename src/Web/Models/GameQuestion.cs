using System;

namespace Web.Models
{
    public class GameQuestion
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public GameSession Game { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}