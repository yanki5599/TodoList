namespace TodoList
{
    enum Column
    {
        Title,
        Date,
        IsDone

    }
    internal interface IRepository<T>
    {
        T GetById(int id);
        T Update(T todo);
        T Add(T todo);
        void DeleteById(int id);
        T GetAll();
        T GetAll(Column column);
    }
}
