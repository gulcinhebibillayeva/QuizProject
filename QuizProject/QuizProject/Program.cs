////using QuizProject.Controller;
////using QuizProject.Database;
////using QuizProject.Enums;
////using QuizProject.Models;
////using QuizProject.Services.Abstract;
////using QuizProject.Services.Concret;

////using System;


////var database = new QuizDatabase();
////var userService = new UserService(database);
////var categoryService = new CategoryService(database);
////var questionService = new QuestionService(database);

////UserController userController = new UserController(userService, categoryService, questionService);
////var adminController = new AdminController(userService, categoryService, questionService);
////while (true)
////{
////    Console.Clear();
////    Console.WriteLine("Quiz App");
////    Console.WriteLine("1.Sign up");
////    Console.WriteLine("2.Sign in ");
////    Console.WriteLine("0. Back");
////    Console.WriteLine("Choices");
////    string choice = Console.ReadLine();
////    switch (choice)
////    {
////        case "1":
////            Console.Clear();
////            Console.Write("İstifadəçi adı: ");
////            string regUsername = Console.ReadLine();

////            Console.Write("Şifrə: ");
////            string regPassword = Console.ReadLine();

////            var newUser = new User
////            {
////                Username = regUsername,
////                Password = regPassword,
////                Id = userService.GetAll().Any() ? userService.GetAll().Max(u => u.Id) + 1 : 1,
////                Role = UserRole.User 
////            };

////            userService.Register(newUser);
////            Console.WriteLine("Qeydiyyat uğurla tamamlandı.");
////            Thread.Sleep(1500);

////            userController.StartQuiz(newUser);
////            break;

////        case "2":
////            Console.Clear();
////            Console.Write("İstifadəçi adı: ");
////            string loginUsername = Console.ReadLine();

////            Console.Write("Şifrə: ");
////            string loginPassword = Console.ReadLine();

////            var loggedInUser = userService.Login(loginUsername, loginPassword);
////            if (loggedInUser != null)
////            {
////                Console.WriteLine("Giriş uğurludur.");
////                Thread.Sleep(1500);

////                if (loggedInUser.Role==UserRole.Admin)
////                {
////                    adminController.AdminMenu();
////                }
////                else
////                {
////                    userController.StartQuiz(loggedInUser);
////                }
////            }
////            else
////            {
////                Console.WriteLine("İstifadəçi adı və ya şifrə yanlışdır.");
////                Thread.Sleep(1500);
////            }
////            break;

////        case "0":
////            Console.WriteLine("Proqramdan çıxılır...");
////            Thread.Sleep(1000);
////            return;

////        default:
////            Console.WriteLine("Yanlış seçim!");
////            Thread.Sleep(1000);
////            break;
////    }
////}

//using QuizProject.Database;

//var database = new QuizDatabase();

//Console.WriteLine($"User file path: {Path.GetFullPath("Users.json")}");
//Console.WriteLine($"Loaded {database.Users.Count} users.");

//foreach (var user in database.Users)
//{
//    Console.WriteLine($"User ID: {user.Id}, Username: {user.Username}, Role: {user.Role}");
//}


using QuizProject.Controller;
using QuizProject.Database;
using QuizProject.Enums;
using QuizProject.Models;
using QuizProject.Services.Abstract;
using QuizProject.Services.Concret;
using System;
using System.Linq;
using System.Threading;

var database = new QuizDatabase();
var userService = new UserService(database);
var categoryService = new CategoryService(database);
var questionService = new QuestionService(database);

var userController = new UserController(userService, categoryService, questionService);
var adminController = new AdminController(userService, categoryService, questionService);

while (true)
{
    Console.Clear();
    Console.WriteLine("=============== QUIZ APP ===============");
    Console.WriteLine("1. Admin");
    Console.WriteLine("2. User");
    Console.WriteLine("0. Çıxış");
    Console.Write("Seçiminizi edin: ");
    string roleChoice = Console.ReadLine();

    switch (roleChoice)
    {
        case "1":
            Console.Clear();
            Console.Write("Admin istifadəçi adı: ");
            string adminUsername = Console.ReadLine();

            Console.Write("Admin şifrə: ");
            string adminPassword = Console.ReadLine();

            try
            {
                var admin = userService.Login(adminUsername, adminPassword);
                if (admin.Role == UserRole.Admin)
                {
                    Console.WriteLine("Admin girişi uğurludur!");
                    Thread.Sleep(1000);
                    adminController.AdminMenu();
                }
                else
                {
                    Console.WriteLine("Bu istifadəçi admin deyil.");
                    Thread.Sleep(1500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
                Thread.Sleep(1500);
            }
            break;

        case "2":
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== İSTİFADƏÇİ MENYUSU ====");
                Console.WriteLine("1. Qeydiyyat (Sign up)");
                Console.WriteLine("2. Giriş (Sign in)");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminizi edin: ");
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("İstifadəçi adı: ");
                        string username = Console.ReadLine();

                        Console.Write("Şifrə: ");
                        string password = Console.ReadLine();

                        var newUser = new User
                        {
                            Username = username,
                            Password = password,
                            Role = UserRole.User,
                            Id = userService.GetAll().Any() ? userService.GetAll().Max(u => u.Id) + 1 : 1
                        };

                        try
                        {
                            userService.Register(newUser);
                            Console.WriteLine("Qeydiyyat uğurludur.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Xəta: {ex.Message}");
                        }
                        Thread.Sleep(1500);
                        break;

                    case "2":
                        Console.Clear();
                        Console.Write("İstifadəçi adı: ");
                        string loginUsername = Console.ReadLine();

                        Console.Write("Şifrə: ");
                        string loginPassword = Console.ReadLine();

                        try
                        {
                            var user = userService.Login(loginUsername, loginPassword);
                            Console.WriteLine($"Xoş gəldiniz, {user.Username}!");
                            Thread.Sleep(1000);
                            userController.StartQuiz(user);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Xəta: {ex.Message}");
                            Thread.Sleep(1500);
                        }
                        break;

                    case "0":
                        goto EndUserMenu;

                    default:
                        Console.WriteLine("Yanlış seçim.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        EndUserMenu:
            break;

        case "0":
            Console.WriteLine("Çıxılır...");
            Thread.Sleep(1000);
            return;

        default:
            Console.WriteLine("Yanlış seçim.");
            Thread.Sleep(1000);
            break;
    }
}

