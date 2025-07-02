

using QuizProject.Database;
using QuizProject.Models;
using QuizProject.Services.Abstract;

namespace QuizProject.Services.Concret;

public class CategoryService : BaseService, ICategoryService
{
    public CategoryService(QuizDatabase database) : base(database)
    {
    }

    public void AddCategory(Category category)
    {
        
    }

    public List<Category> GetAll()
    {
        return _database.CategoryQuestion;
    }

    public Category GetById(int id)
    {
        return _database.CategoryQuestion.FirstOrDefault(c => c.CategoryId == id);
    }

    public void RemoveCategory(int id)
    {
        var category = _database.CategoryQuestion.FirstOrDefault(c => c.CategoryId == id);
        if(category!=null)
        {
            _database.CategoryQuestion.Remove(category);
            _database.SaveAll();
        }
    }
}
