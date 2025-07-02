

using QuizProject.Helpers;
using QuizProject.Models;

namespace QuizProject.Database;

public class QuizDatabase
{
    private const string UsersFile = "Users.json";
    private const string QuestionsFile = "questions.json";
    private const string CategorysFile = "Catagories.json";
    public List<User> Users { get; set; }
    public List<Questions> Questionss { get; set; }
    public List<Category> CategoryQuestion { get; set; }
    public QuizDatabase()
    {
       
        Users = JsonHelper.ReadFromJson<User>(UsersFile) ?? new List<User>();
        Questionss = JsonHelper.ReadFromJson<Questions>(QuestionsFile) ?? new List<Questions>();
        CategoryQuestion = JsonHelper.ReadFromJson<Category>(CategorysFile) ?? new List<Category>();

        Console.WriteLine($"Loaded {CategoryQuestion.Count} categories.");
    }


public void SaveAll()
    {
        JsonHelper.WriteToJson(UsersFile, Users);
        JsonHelper.WriteToJson(QuestionsFile, Questionss);
        JsonHelper.WriteToJson(CategorysFile, CategoryQuestion);
    }
}