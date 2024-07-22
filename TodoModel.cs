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

        [XmlElement("Date")] 
        public string XmlDate
        {
            get { return Date.ToString("yyyy-MM-dd"); } 
            set { Date = DateOnly.Parse(value); } 
        }

        [XmlIgnore] 
        public DateOnly Date { get; set; }

        public TodoModel()
        {

        }
    }
}
