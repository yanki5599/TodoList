using ReaLTaiizor.Forms;

namespace TodoList
{
    enum Mode
    {
        Add,
        Edit
    }
    internal partial class Todos : MaterialForm
    {
        private List<TodoModel> todos;
        private Mode mode;
        private IRepository<TodoModel> repository;

        public Todos(IRepository<TodoModel> repository)
        {
            InitializeComponent();
            todos = new List<TodoModel>();
            dataGridView_tasks.DataSource = todos;
            this.repository = repository;
        }

        private void populateViewWithTodo() { }

        // populate form from selected row
        private void dataGridView_tasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetMode(Mode mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case Mode.Add:
                    button_action.Text = "Add";
                    break;
                case Mode.Edit:
                    button_action.Text = "Edit";
                    break;
            }
        }

        // add or edit based on mode
        private void button_action_Click(object sender, EventArgs e)
        {

        }
    }
}
