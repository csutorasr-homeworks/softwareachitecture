using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public QuestionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Question>> GetQuestionForUser(Guid userId)
        {
            return await dbContext.Questions.Where(x => x.AddedById == userId).ToListAsync();
        }

        public async Task AddQuestion(Guid userId, string text, string correctAnswer, IEnumerable<string> incorrectAnswers)
        {
            var question = new Question
            {
                AddedById = userId,
                QuestionCategoryId = (await dbContext.QuestionCategories.FirstAsync()).Id,
                Answers = incorrectAnswers.Select(x => new Answer
                {
                    IsCorrect = false,
                    Text = x,
                }).ToList(),
                Text = text,
            };
            question.Answers.Add(new Answer
            {
                IsCorrect = true,
                Text = correctAnswer,
            });
            await dbContext.Questions.AddAsync(question);
            await dbContext.SaveChangesAsync();
        }
    }
}
