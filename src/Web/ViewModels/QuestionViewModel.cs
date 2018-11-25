using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        public Guid Text { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public class Answer
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
            public IEnumerable<string> UserIdsSelected { get; set; }
        }
        public Guid CorrectAnswerId { get; set; }
    }
}
