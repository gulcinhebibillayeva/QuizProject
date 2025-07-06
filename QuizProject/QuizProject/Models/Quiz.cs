

namespace QuizProject.Models;

public class Quiz
{
   public int quizId { get; set; }
    public string title { get; set; }
    public int CategoryId { get; set; }
    public List<int> QuestionIds { get; set; }

}
