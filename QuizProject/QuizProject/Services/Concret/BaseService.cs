
using QuizProject.Database;
namespace QuizProject.Services.Concret
{
    public abstract class BaseService
    {
        protected QuizDatabase _database;   
        public BaseService (QuizDatabase database)
        {
            _database = database;
        }
    }
}
