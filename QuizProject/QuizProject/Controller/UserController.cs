

using QuizProject.Models;
using QuizProject.Services.Abstract;
using QuizProject.Services.Concret;

namespace QuizProject.Controller;

public class UserController
{
    private readonly ICategoryService _categoryService;
    private readonly IQuestionService _questionService;
    private readonly IUserService _userService;

    public UserController(Services.Concret.UserService userService, ICategoryService categoryService, IQuestionService questionService)
    {
        _userService = userService;
        _categoryService = categoryService;
        _questionService = questionService;
    }

    public User Login()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        try
        {
            var user = _userService.Login(username, password);
            Console.WriteLine($"Welcome {user.Username}!");
            Thread.Sleep(1500);
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Thread.Sleep(1500);
            return null;
        }
    }
    public void StartQuiz(User user)
    {
        var categories = _categoryService.GetAll();
        if (!categories.Any())
        {
            Console.WriteLine("There is no any category.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine("This category is not found:");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.CategoryId}. {category.Name}");
        }
        Console.Write("Enter id: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Wrong choice.");
            Thread.Sleep(1500);
            return;
        }
        var questions = _questionService.GetAll()
            .Where(q => q.CategoryId == categoryId)
            .ToList();

        if (!questions.Any())
        {
            Console.WriteLine("There is no any question in this category.");
            Thread.Sleep(1500);
            return;
        }
        int score = 0;

        foreach (var question in questions)
        {
            Console.Clear();
            Console.WriteLine(question.Text);

            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i]}");
            }

            Console.Write("Enter your choice (1-4): ");
            if (int.TryParse(Console.ReadLine(), out int userAnswer) && userAnswer - 1 == question.CorrectOptionIndex)
            {
                score++;
            }
        }
        Console.Clear();
        Console.WriteLine($"End of quiz ! Your score: {score} / {questions.Count}");
        Thread.Sleep(3000);


    }
    

}
