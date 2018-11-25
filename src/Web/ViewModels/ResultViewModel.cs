using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel(GameSession gameSession)
        {
            Users = gameSession.Users.Select(x => new UserViewModel(x)).ToList();
            QuestionsWithResult = gameSession.Questions.Select(q => new QuestionWithResult
            {
                QuesetionId = q.QuestionId,
                Text = q.Question.Text,
                PointsByUser = q.UserSelectedAnswers.ToDictionary(u => u.UserGameSession.UserId, u => u.Points),
            }).ToList();
            SumPointsByUser = gameSession.Users.ToDictionary(
                u => u.UserId,
                u => QuestionsWithResult.SelectMany(q => q.PointsByUser).Where(q => q.Key == u.UserId).Sum(x => x.Value)
            );
        }
        public ICollection<UserViewModel> Users { get; set; }
        public ICollection<QuestionWithResult> QuestionsWithResult { get; set; }

        public class QuestionWithResult
        {
            public Guid QuesetionId { get; set; }
            public string Text { get; set; }
            public IDictionary<string, int> PointsByUser { get; set; }
        }
        public IDictionary<string, int> SumPointsByUser { get; set; }
    }
}
