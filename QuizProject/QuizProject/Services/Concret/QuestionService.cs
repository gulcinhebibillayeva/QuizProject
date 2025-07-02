using QuizProject.Database;
using QuizProject.Models;
using QuizProject.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Services.Concret
{
    public class QuestionService : BaseService, IQuestionService
    {
        public QuestionService(QuizDatabase database) : base(database)
        {
        }

       

        public void AddQuestion(Questions question)
        {
            if (_database.Questionss.Any())
            {
                question.Id = _database.Questionss.Max(q => q.Id) + 1;
            }
            else
            {
                question.Id = 1;
            }

            _database.Questionss.Add(question);
            _database.SaveAll();



        }



        public List<Questions> GetByCategory(int categoryId)
        {
            return _database.Questionss.Where(
                q => q.CategoryId == categoryId).ToList();
            
        }

        public Questions GetById(int id)
        {
            return _database.Questionss.FirstOrDefault(q => q.Id == id);
        }

        public void RemoveQuestion(int  id)
        {
            var question = _database.Questionss.FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                _database.Questionss.Remove(question);
                _database.SaveAll();
            }
        }
        List<Questions> IQuestionService.GetAll()
        {
            return _database.Questionss;
        }
    }
}
