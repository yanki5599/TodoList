using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Repositories
{
    internal class JsonRepository : IRepository<TodoModel>
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
