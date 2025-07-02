

using QuizProject.Models;

namespace QuizProject.Services.Abstract;

public interface IQuestionService
{
  
    public void AddQuestion(Questions question);
    public List<Questions> GetAll();
    public List<Questions> GetByCategory(int categoryId);
   public  Questions GetById(int id);
    public void RemoveQuestion(int id);
}
