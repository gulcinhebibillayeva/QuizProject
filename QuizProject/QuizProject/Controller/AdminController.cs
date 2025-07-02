
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

    public AdminController(
            IUserService userService,
            ICategoryService categoryService,
            IQuestionService questionService)
    {
        _userService = userService;
        _categoryService = categoryService;
        _questionService = questionService;
    }

    public void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== ADMIN PANEL =====");
            Console.WriteLine("1. Kateqoriya əlavə et");
            Console.WriteLine("2. Kateqoriya sil");
            Console.WriteLine("3. Sual əlavə et");
            Console.WriteLine("4. Sual sil");
            Console.WriteLine("5. İstifadəçiləri göstər");
            Console.WriteLine("6. İstifadəçi sil");
            Console.WriteLine("0. Çıxış");
            Console.Write("Seçiminizi daxil edin: ");

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
                    RemoveUser();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim!");
                    Thread.Sleep(1000);
                    break;
            }
        }
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
