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

        private DateOnly _date;
        [XmlElement("Date")] 
        public DateOnly Date {
            get { return _date; } 
            set 
            {
                bool success = DateOnly.TryParse(value.ToString(),out DateOnly result);
                if(success)
                    _date = result;
                
            } 
        }

        [XmlElement("IsDone")]
        public bool IsDone { get; set; }
        public TodoModel()
        {
            IsDone = false;
        }


    }
}
