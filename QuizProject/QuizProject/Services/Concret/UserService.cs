

using QuizProject.Database;
using QuizProject.Models;
using QuizProject.Services.Abstract;
using System.Linq;

namespace QuizProject.Services.Concret;

public class UserService : BaseService, IUserService
{
    public UserService(QuizDatabase database) : base(database)
    {
    }

    public void Delete(int id)
    {
        var user = _database.Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _database.Users.Remove(user);
            _database.SaveAll();
        }
    }

    public List<User> GetAll()
    {
        return _database.Users;
    }

    public User GetById(int id)
    {
        return _database.Users.FirstOrDefault(u => u.Id == id);
    }

    public User GetByUsername(string username)
    {
        return _database.Users.FirstOrDefault(u => u.Username == username);
    }

    public User Login(string username, string password)
    {
        var user = _database.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null)
            throw new Exception("Invalid username or password");
        return user;
    }

    public void Register(User user)
    {
        if (_database.Users.Any(u => u.Username == user.Username))
        {
            throw new Exception("User already exists");
        }

        user.Id = _database.Users.Any()
            ? _database.Users.Max(u => u.Id) + 1
            : 1;

        _database.Users.Add(user);
        _database.SaveAll();
    }
}
