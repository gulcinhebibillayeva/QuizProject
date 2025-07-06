

using QuizProject.Models;
using QuizProject.Services.Abstract;
using QuizProject.Services.Concret;

namespace QuizProject.Controller;

public class UserController
{
    private readonly ICategoryService _categoryService;
    private readonly IQuestionService _questionService;
    private readonly IUserService _userService;
    private readonly IQuizService _quizService;
   public UserController(Services.Concret.UserService userService, ICategoryService categoryService, IQuestionService questionService,IQuizService quizService)
    {
        _userService = userService;
        _categoryService = categoryService;
        _questionService = questionService;
        _quizService = quizService;
       
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

    public void ShowAvailableQuizzes(User user)
    {
        Console.Clear();
        var quizzes = _quizService.GetAll();
        if (!quizzes.Any())
        {
            Console.WriteLine("There is no found any quiz.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine("===  Quizzes ===");
        foreach (var q in quizzes)
        {
            Console.WriteLine($"{q.quizId}. {q.title}");
        }
        Console.Write("Enter quiz id: ");
        if (int.TryParse(Console.ReadLine(), out int quizId))
        {
            var quiz = _quizService.GetById(quizId);
            if (quiz != null)
                StartQuiz(quiz);
            else
            {
                Console.WriteLine("There is no found any quiz.");
                Thread.Sleep(1500);
            }
        }
    }
    public void StartQuiz(Quiz quiz)
    {
        Console.Clear();
        Console.WriteLine($"=== Quiz: {quiz.title} ===\n");

        var allQuestions = _questionService.GetAll();
        var quizQuestions = allQuestions
                .Where(q => quiz.QuestionIds.Contains(q.Id))
                .ToList();

        int score = 0;
        for (int i = 0; i < quizQuestions.Count; i++)
        {
            var q = quizQuestions[i];
            Console.Clear();
            Console.WriteLine($"Question {i + 1}/{quizQuestions.Count}: {q.Text}\n");
            for (int opt = 0; opt < q.Options.Count; opt++)
            {
                Console.WriteLine($"{opt + 1}. {q.Options[opt]}");
            }

            Console.Write("\nYour Answer (1-4): ");
            if (int.TryParse(Console.ReadLine(), out int ans) &&
                ans - 1 == q.CorrectOptionIndex)
            {
                score++;
            }



        }
        Console.Clear();
        Console.WriteLine($"End of the quiz your score: {score} / {quizQuestions.Count}");
        Console.WriteLine("For continiue press any key...");
        Console.ReadLine();
    }

    internal void StartQuiz(User user)
    {
        throw new NotImplementedException();
    }
}
