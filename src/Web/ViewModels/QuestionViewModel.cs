using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel(GameQuestion question, bool showCorrect = false)
        {
            GameQuestionId = question.Id;
            QuestionId = question.Question.Id;
            Text = question.Question.Text;
            QuestionStartTime = question.StartTime.Value;
            Answers = question.Question.Answers.Select(x => new Answer
            {
                Id = x.Id,
                Text = x.Text,
                UserIdsSelected = question.UserSelectedAnswers.Where(u => u.AnswerId == x.Id).ToDictionary(u => u.UserGameSession.UserId, u => u.AnswerTime)
            });
            if (showCorrect)
            {
                CorrectAnswerId = question.Question.Answers.First(x => x.IsCorrect).Id;
            }
        }
        public Guid GameQuestionId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public DateTime QuestionStartTime { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public class Answer
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
            public IDictionary<string, DateTime> UserIdsSelected { get; set; }
        }
        public Guid? CorrectAnswerId { get; set; }
    }
}
