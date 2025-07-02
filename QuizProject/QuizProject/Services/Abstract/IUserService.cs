using QuizProject.Models;

namespace QuizProject.Services.Abstract;

public interface IUserService
{
    public void Register(User user);
    public User Login(string username, string password);
    List<User> GetAll();
    User GetByUsername(string username);
    public void Delete(int id);
    public User GetById(int id);
}
