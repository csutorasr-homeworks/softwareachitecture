using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void SeedDatabase(this ApplicationDbContext dbContext)
        {
            if (dbContext.QuestionCategories.Count() == 0)
            {
                var text = File.ReadAllText("questions.json");
                var json = JsonConvert.DeserializeObject<IEnumerable<SeedQuestion>>(text);
                var categoryNames = json.Select(x => x.category).Distinct();
                var categories = categoryNames.Select(x => new Models.QuestionCategory
                {
                    Name = x,
                }).ToList();
                dbContext.QuestionCategories.AddRange(categories);
                var questions = json.Select(q => new Models.Question
                {
                    QuestionCategory = categories.FirstOrDefault(x => x.Name == q.category),
                    Text = q.question,
                    Answers = q.incorrect_answers.Select(x => new Models.Answer
                    {
                        IsCorrect = false,
                        Text = x,
                    }).Union(new List<Models.Answer>
                    {
                        new Models.Answer
                        {
                            IsCorrect = true,
                            Text = q.correct_answer,
                        }
                    }).ToList(),
                }).ToList();
                dbContext.Questions.AddRange(questions);
                dbContext.SaveChanges();
            }
        }
    }

    class SeedQuestion
    {
        public string category { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public IEnumerable<string> incorrect_answers { get; set; }
    } 
}
