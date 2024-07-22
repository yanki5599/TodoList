namespace TodoList
{
   
    internal interface IRepository<T>
    {
        T GetById(int id);
        T Update(T todo);
        T Add(T todo);
        void DeleteById(int id);
        List<T> GetAll();
        // get by col and value
        List<T> GetAll(Func<T, bool> predicate);
        int GenerateId(); // add
    }
}
