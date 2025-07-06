
using QuizProject.Services.Abstract;
using QuizProject.Services.Concret;
using QuizProject.Models;
using System.Linq;
using QuizProject.Database;
using QuizProject.Models;

namespace QuizProject.Controller;

public class AdminController
{
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IQuestionService _questionService;
    private readonly IQuizService _quizService;
    public AdminController(
            IUserService userService,
            ICategoryService categoryService,
            IQuestionService questionService,
            IQuizService quizService)
    {
        _userService = userService;
        _categoryService = categoryService;
        _questionService = questionService;
        _quizService = quizService;
    }

    public void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Admin menu =====");
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Remove Category");
            Console.WriteLine("3. Add Question");
            Console.WriteLine("4. Remove Question");
            Console.WriteLine("5. Show users");
            Console.WriteLine("6. Remove Users");
            Console.WriteLine("7. Show Questions");
            Console.WriteLine("8. Add quiz");
            Console.WriteLine("0. Back");
            Console.Write("Enter your choice : ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    RemoveCategory();
                    break;
                case "3":
                    AddQuestion();
                    break;
                case "4":
                    RemoveQuestion();
                    break;
                case "5":
                    ShowUsers();
                    break;
                case "6":
                    RemoveQuestion();
                    break;
                    case "7":
                    ShowAllQuestions();
                    break;
                case "8":
                    CreateQuiz();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Wrong input!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void ShowAllQuestions()
    {
        Console.Clear();
        var all = _questionService.GetAll();
        if (all == null || !all.Any())
        {
            Console.WriteLine("There is no found any question.");
        }
        else
        {
            Console.WriteLine("=== All Questions ===");
            foreach (var q in all)
            {
                Console.WriteLine($"ID: {q.Id} | CatId: {q.CategoryId} | {q.Text}");
                for (int i = 0; i < q.Options.Count; i++)
                    Console.WriteLine($"\t{i + 1}. {q.Options[i]}");
                Console.WriteLine(new string('-', 40));
            }
        }
        Console.WriteLine("For continue press any key...");
        Console.ReadKey();
    }
    private void RemoveUser()
    {
        Console.Clear();
        Console.WriteLine("Enter Id:");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" Wrong input!");
            Thread.Sleep(1500);
            return;
        }

        var user = _userService.GetById(id);
        if (user == null)
        {
            Console.WriteLine("User does not found.");
            Thread.Sleep(1500);
            return;
        }

        _userService.Delete(id);
        Console.WriteLine("User removed successfully.");
        Thread.Sleep(1500);
    }

    private void ShowUsers()
    {
        Console.Clear();
        var users = _userService.GetAll();

        if (users == null || users.Count == 0)
        {
            Console.WriteLine("Usernot found.");
        }
        else
        {
            Console.WriteLine("=== Users ===");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id} | Username: {user.Username} | Email: {user.Username}");
            }
        }

        Console.WriteLine("\nFor continue enter any key...");
        Console.ReadKey();
    }

    private void RemoveQuestion()
    {
        Console.Clear();
        Console.WriteLine("Enter id :");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("wrong input!");
            Thread.Sleep(1500);
            return;
        }
        var question = _questionService.GetById(id);
        if (question == null)
        {
            Console.WriteLine("This Id does not found.");
            Thread.Sleep(1500);
            return;
        }
        _questionService.RemoveQuestion(id);
        Console.WriteLine("Question removed successfully.");
        Thread.Sleep(1500);

    }

    private void AddQuestion()
    {
        Console.Clear();
        Console.WriteLine("Add question:");
        string questionText = Console.ReadLine();
        var category = _categoryService.GetAll();
        for (int i = 0; i < category.Count; i++)
        {
            Console.WriteLine($"{category[i].CategoryId}.{category[i].Name}");

        }
        Console.WriteLine("Add category id ");
        int categoryId = int.Parse(Console.ReadLine());
        List<string> answers = new List<string>();
        for (int i = 1; i <= 4; i++)
        {
            Console.Write($"Variant {i}: ");
            string answer = Console.ReadLine();
            answers.Add(answer);
        }

        Console.Write("Enter number of true answer (1-4): ");
        int correctAnswerIndex = int.Parse(Console.ReadLine()) - 1;
        var question = new Questions
        {
            Id = _questionService.GetAll().Any()
           ? _questionService.GetAll().Max(q => q.Id) + 1
           : 1,
            Text = questionText,
            CategoryId = categoryId,
            Options = answers,
            CorrectOptionIndex = correctAnswerIndex
        };

        _questionService.AddQuestion(question);

        Console.WriteLine("Question added!");
        Thread.Sleep(1500);
    

    }

    private void RemoveCategory()
    {
        Console.Clear();
        Console.WriteLine("Enter id :");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("wrong input!");
            Thread.Sleep(1500);
            return;
        }
        var category = _categoryService.GetById(id);
        if (category == null)
        {
            Console.WriteLine("This Id does not found.");
            Thread.Sleep(1500);
            return;
        }
        _categoryService.RemoveCategory(id);
        Console.WriteLine("Category added successfully.");
        Thread.Sleep(1500);

    }


    private void CreateQuiz()
    {
        Console.Clear();
        var categories = _categoryService.GetAll();
        if (categories.Count == 0)
        {
            Console.WriteLine("Category is not found.");
            Thread.Sleep(1500);
            return;
        }
        Console.WriteLine("Choose Category");
        foreach(var c in categories)
        {
            Console.WriteLine($"{c.CategoryId}.{c.Name}");
        }

        Console.Write("Category ID: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Wrong  ID");
            Thread.Sleep(1500);
            return;
        }

        var questions = _questionService.GetAll()
       .Where(q => q.CategoryId == categoryId)
       .ToList();
        if (questions.Count < 20)
        {
            Console.WriteLine("Question count is less than 20 in this category.");
            Thread.Sleep(1500);
            return;
        }
        Console.WriteLine("Questions:");
        foreach (var q in questions)
        {
            Console.WriteLine($"{q.Id}. {q.Text}");
        }
        Console.WriteLine("Enter 20 Id (with ,):");
        string[] idStrings = Console.ReadLine().Split(',');

        if (idStrings.Length != 20)
        {
            Console.WriteLine("Please enter 20 question id");
            Thread.Sleep(1500);
            return;
        }
        List<int> questionIds = new();
        foreach (var idStr in idStrings)
        {
            if (int.TryParse(idStr.Trim(), out int qid))
            {
                if (questions.Any(q => q.Id == qid))
                {
                    questionIds.Add(qid);
                }
            }
        }

        if (questionIds.Count != 20)
        {
            Console.WriteLine("Some ids are wrong");
            Thread.Sleep(1500);
            return;
        }

        var quiz = new Quiz
        {
            quizId = _quizService.GetAll().Any()
                 ? _quizService.GetAll().Max(q => q.quizId) + 1
                 : 1,
            CategoryId = categoryId,
            QuestionIds = questionIds
        };

        _quizService.AddQuiz(quiz);
        Console.WriteLine("Quiz created!");
        Thread.Sleep(1500);



    }
    private void AddCategory()
    {
        Console.Clear();
        Console.WriteLine("Add new Category");
        Console.WriteLine("Name:");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Category name is empty");
                Thread.Sleep(1500);
            return;
        }
        var existing = _categoryService.GetAll()
    .FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        if (existing != null)
        {
            Console.WriteLine("This category alredy exist ");
            Thread.Sleep(1500);
            return;
        }
        var category = new Category
        {
            CategoryId = _categoryService.GetAll().Any()
            ? _categoryService.GetAll().Max(c => c.CategoryId) + 1
            : 1,
            Name = name
        };
        _categoryService.AddCategory(category);
        Console.WriteLine("Category added successfully");
        Thread.Sleep(1500);

    }

   
}
