
namespace QuizProject.Models;

public class Questions
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Text { get; set; }
    public List<string> Options { get; set; }


    public int CorrectOptionIndex
    {
        get; set;
    }
}
