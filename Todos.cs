using ReaLTaiizor.Forms;
using System.Data;
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
        public int ID;
        public Todos(IRepository<TodoModel> repository)
        {
            InitializeComponent();
            this.repository = repository;
            LoadData();
            SetMode(Mode.Add);
        }
        public void LoadData()
        {
            todos = repository.GetAll();
            dataGridView_tasks.DataSource = todos;
        }
        private void populateViewWithTodo(DataGridViewRow row)
        {
            ID = (int)row.Cells[0].Value;
            textbox_title.Text = row.Cells[1].Value.ToString();
            hopeDatePicker1.Date = DateTime.Parse(row.Cells[2].Value.ToString());
            checkbox_isDone.Checked = (bool)row.Cells[4].Value;
        }
        private void dataGridView_tasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx < 0) return;
            DataGridViewRow row = dataGridView_tasks.Rows[e.RowIndex];
            if (row != null)
            {
                SetMode(Mode.Edit);
                populateViewWithTodo(row);
            }
        }
        private void SetMode(Mode newMode)
        {
            switch (newMode)
            {
                case Mode.Add:
                    button_action.Text = "Add";
                    cancelBTN.Text = "CLEAR";
                    cancelBTN.Enabled = false;
                    break;
                case Mode.Edit:
                    button_action.Text = "Edit";
                    cancelBTN.Text = "Cancel";
                    cancelBTN.Enabled = true;
                    break;
            }
            this.mode = newMode;
        }
        private void button_action_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Add)
            {
                Add_new_todo();
                SetMode(Mode.Add);
            }
            if (mode == Mode.Edit)
            {
                Update_todo();
                SetMode(Mode.Add);
            }
        }
        private void Add_new_todo()
        {
            string title = textbox_title.Text;
            string date = hopeDatePicker1.Date.ToShortDateString();
            if (!string.IsNullOrEmpty(title))
            {
                TodoModel newTodo = new TodoModel()
                { Title = title, XmlDate = date, IsDone = checkbox_isDone.Checked };
                repository.Add(newTodo);
                MessageBox.Show("New Task added!");
                ClearForm();
                LoadData();
            }
            else
            {
                MessageBox.Show("Task name cannot be empty.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void ClearForm()
        {
            textbox_title.Text = "";
            hopeDatePicker1.Date = DateTime.Now;
            checkbox_isDone.Checked = false;

            dataGridView_tasks.ClearSelection();
        }
        private void Update_todo()
        {
            string title = textbox_title.Text;
            string date = hopeDatePicker1.Date.ToShortDateString();
            if (!string.IsNullOrEmpty(title))
            {
                TodoModel newTodo = new TodoModel()
                { Id = ID, Title = title, XmlDate = date, IsDone = checkbox_isDone.Checked };
                repository.Update(newTodo);
                MessageBox.Show("Todo task updated!");
                ClearForm();
                LoadData();
            }
        }
        private void cancelBTN_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Edit)
            {// cancel pressed
                ClearForm();
                SetMode(Mode.Add);
            }
            if (mode == Mode.Add)
            {// clear pressed
                ClearForm();
                SetMode(Mode.Add);
            }
        }
        private void UpdateCancelButton(object sender, EventArgs e)
        {
            cancelBTN.Enabled = (!string.IsNullOrEmpty(textbox_title.Text) || hopeDatePicker1.Date != DateTime.Now)
        }
        private void RightMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
                dataGridView_tasks.ClearSelection();
                dataGridView_tasks.Rows[e.RowIndex].Selected = true;
                ID = (int)dataGridView_tasks.SelectedRows[0].Cells[0].Value;
            }
        }
        private void DeleteTodo(object sender, EventArgs e)
        {
            repository.DeleteById(ID);
            LoadData();
        }
    }
}