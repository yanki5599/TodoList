using System.Xml.Serialization;

namespace TodoList
{
    [XmlRoot("Todo")] 
    public class TodoModel
    {
        [XmlElement("ID")] 
        public int Id { get; set; }

        [XmlElement("Title")] 
        public string Title { get; set; }

        [XmlIgnore] 
        public DateOnly Date { get; set; }

        [XmlElement("IsDone")]
        public bool IsDone { get; set; }
        public TodoModel()
        {
            IsDone = false;
        }


    }
}
