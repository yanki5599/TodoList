using System.Xml.Linq;

namespace TodoList.Repositories
{
    internal class XMLRepository  : IRepository<TodoModel>
    {
        private const string PATH = ".\\todo.xml";
        private XDocument _activeDoc;

        public XMLRepository() 
        {
            EnsureFile();
            _activeDoc = XDocument.Load(PATH);
        }

        private void EnsureFile()
        {
            if (!File.Exists(PATH))
            {
                var root = new XDocument(new XElement("Quiz"));
                root.Save(PATH);
            }
            
        }
        public TodoModel Add(TodoModel todo)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public TodoModel GetAll()
        {
            throw new NotImplementedException();
        }

        public TodoModel GetAll(Column column)
        {
            return  _activeDoc.Root.Elements().Select(item=>)
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
