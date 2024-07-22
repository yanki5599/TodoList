using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TodoList.Repositories
{
    internal class XMLRepository :IRepository<TodoModel>
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
                var root = new XDocument(new XElement("Todos"));
                root.Save(PATH);
            }
            
        }
        public TodoModel Add(TodoModel todo)
        {
            XElement root = _activeDoc.Root;
            int lastID = GenerateId();
            todo.Id = lastID + 1;
            XElement newID = new XElement("ID", lastID + 1);
            XElement newTitle = new XElement("Title", todo.Title);
            XElement newDate = new XElement("Date", todo.XmlDate);
            XElement isDone = new XElement("IsDone", todo.IsDone);
            XElement parent = new XElement("Todo", newID, newTitle, newDate,isDone);
            root.Add(parent);
            _activeDoc.Save(PATH);
            return todo;
        }

        public void DeleteById(int id)
        {
            _activeDoc.Root.Elements("Todo")
                .Single(item=>item.Element("ID").Value == id.ToString()).Remove();
            _activeDoc.Save(PATH);
        }

        public List<TodoModel> GetAll()
        {
            List<TodoModel> todoList = new List<TodoModel>();
            try
            {
                if (File.Exists(PATH))
                {
                    Console.WriteLine("File found, loading XML...");
                    XDocument doc = XDocument.Load(PATH);
                    foreach (XElement tu in doc.Root.Descendants("Todo"))
                    {
                        int id = Int32.Parse(tu.Element("ID")?.Value ?? "");
                        string title = tu.Element("Title")?.Value ?? "";
                        string date = tu.Element("Date")?.Value ?? "";
                        bool done = bool.Parse(tu.Element("IsDone")?.Value);
                        TodoModel todoModel = new TodoModel() 
                        { Id = id , Title = title , XmlDate = date , IsDone=done};
                        todoList.Add(todoModel);
                    }
                    return todoList;
                }
                else
                {
                    Console.WriteLine("File not found: " + PATH);
                    return null;
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine("XML Exception: " + ex.Message);
                return null;
            }

        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public TodoModel GetById(int id)
        {
            return GetAll(x=>(x.Id == id)).Single();    
        }

        public TodoModel Update(TodoModel todo)
        {
            var elem = _activeDoc.Root.Elements("Todo")
                 .Single(item => int.Parse(item.Element("ID").Value) == todo.Id);
            elem.Element("Title").Value = todo.Title;
            elem.Element("Date").Value = todo.XmlDate;
            elem.Element("IsDone").Value = todo.IsDone.ToString();
            _activeDoc.Save(PATH);
            return todo;
        }
        
        public int GenerateId()
        {
           var list = GetAll();
           return list.Count <= 0 ? 1 : GetAll().Max(x => x.Id) +1;
        }
    }
}
