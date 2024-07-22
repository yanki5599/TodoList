
namespace TodoList.Repositories
{
    internal class XMLRepository : IRepository<TodoModel>
    {
        public TodoModel Add(TodoModel todo)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TodoModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TodoModel Update(TodoModel todo)
        {
            throw new NotImplementedException();
        }
    }
}
