using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories
{
    public interface IQuestionRepository
    {
        Task AddQuestion(Guid userId, string text, string correctAnswer, IEnumerable<string> incorrectAnswers);
        Task<IEnumerable<Question>> GetQuestionForUser(Guid userId);
    }
}