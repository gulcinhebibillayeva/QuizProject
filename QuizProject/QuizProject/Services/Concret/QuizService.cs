
using QuizProject.Database;
using QuizProject.Models;
using QuizProject.Services.Abstract;

namespace QuizProject.Services.Concret;

public class QuizService : BaseService, IQuizService
{
    public QuizService(QuizDatabase database) : base(database)
    {
    }

    public void AddQuiz(Quiz quiz)
    {
        _database.Quizzes.Add(quiz);
        _database.SaveAll();
    }

    public List<Quiz> GetAll()
    {
        return _database.Quizzes;
    }

    public Quiz GetById(int id)
    {
        return _database.Quizzes.FirstOrDefault(q => q.quizId == id);
    }

    public void RemoveQuiz(int id)
    {
        var quiz = _database.Quizzes.FirstOrDefault(q => q.quizId == id);
        if (quiz != null)
        {
            _database.Quizzes.Remove(quiz);
            _database.SaveAll();
        }
    }
}
