

using QuizProject.Models;

namespace QuizProject.Services.Abstract;

public  interface ICategoryService
{
    public void AddCategory(Category category);
    public  void RemoveCategory(int id);
    List<Category> GetAll();
    Category GetById(int id);
    

}
