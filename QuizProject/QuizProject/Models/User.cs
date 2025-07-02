

using QuizProject.Enums;

namespace QuizProject.Models;

public class User:BaseModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
   public  UserRole Role { get; set; }
}
