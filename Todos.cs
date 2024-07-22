using ReaLTaiizor.Forms;
using System.Data;
namespace TodoList
{
    enum Mode
    {
        Add,
        Edit
    }

    
    enum ColumnName
    {
        ID = 0,
        Title = 1,
        Date = 2,
        Done = 3,
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
            ID                      = (int)row.Cells[(int)ColumnName.ID].Value;
            textbox_title.Text      = row.Cells[(int)ColumnName.Title].Value.ToString();
            hopeDatePicker1.Date    = DateTime.Parse(row.Cells[(int)ColumnName.Date].Value.ToString());
            checkbox_isDone.Checked = (bool)row.Cells[(int)ColumnName.Done].Value;

        }
        private void dataGridView_tasks_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    button_action.Text = "Update";
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
            var date = hopeDatePicker1.Date;
            if (!string.IsNullOrEmpty(title))
            {
                TodoModel newTodo = new TodoModel()
                { Title = title, Date = DateOnly.Parse(date.ToShortDateString()), IsDone = checkbox_isDone.Checked };
                repository.Add(newTodo);
                MessageBox.Show("New Task added!");
                ClearForm();
                LoadData();
            }
            else
            {
                MessageBox.Show("Task name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                { Id = ID, Title = title, Date = DateOnly.Parse(date), IsDone = checkbox_isDone.Checked };
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
            cancelBTN.Enabled = (!string.IsNullOrEmpty(textbox_title.Text) || hopeDatePicker1.Date != DateTime.Now);
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

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView_tasks.ReadOnly = false;
            foreach (DataGridViewRow row in dataGridView_tasks.Rows)
            {
                if (!row.Selected)
                    row.ReadOnly = true;
                else
                {
                    row.Cells[(int)ColumnName.ID].ReadOnly = true;
                }
            }
        }

        private void dataGridView_tasks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int idInRow = (int)(dataGridView_tasks.Rows[e.RowIndex].Cells[0].Value);
            var model = ((List<TodoModel>)dataGridView_tasks.DataSource)
                .Find(model => model.Id == idInRow);
            if (model == null)
                throw new Exception("something went teribly wrong!");
            repository.Update(model);
            dataGridView_tasks.ReadOnly = true;
        }
    }
}