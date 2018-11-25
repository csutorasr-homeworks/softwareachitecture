using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Guid? AddedById { get; set; }
        public User AddedBy { get; set; }
    }
}
