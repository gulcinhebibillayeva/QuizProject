
namespace QuizProject.Services.Abstract;
using QuizProject.Models;

public interface IQuizService
{
    void AddQuiz(Quiz quiz);
    void RemoveQuiz(int id);
    List<Quiz> GetAll();
    Quiz GetById(int id);
}
