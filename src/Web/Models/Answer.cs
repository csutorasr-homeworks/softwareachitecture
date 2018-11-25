using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<UserSelectedAnswer> UserSelectedAnswers { get; set; }
    }
}