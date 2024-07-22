using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace TodoList.Repositories
{
    internal class JsonRepository : IRepository<TodoModel>
    {
        private const string PATH = ".\\todo.json"; 

        private void EnsureFile()
        {
            if (!File.Exists(PATH))
            {
                File.WriteAllText(PATH, "[]");
            }
        }

        public TodoModel Add(TodoModel todo)
        {
            EnsureFile();

            List<TodoModel> todos = GetAll();

            todo.Id = GenerateId();

            todos.Add(todo);

            SaveAll(todos);

            return todo;
        }

        public void DeleteById(int id)
        {
            EnsureFile();

            List<TodoModel> todos = GetAll();

            todos.RemoveAll(t => t.Id == id);

            SaveAll(todos);
        }

        public int GenerateId()
        {
            EnsureFile();

            List<TodoModel> todos = GetAll();

            if (todos.Count == 0)
            {
                return 1; 
            }
            else
            {
                int maxId = todos.Max(t => t.Id);
                return maxId + 1;
            }
        }

        public List<TodoModel> GetAll()
        {
            EnsureFile();

            string json = File.ReadAllText(PATH);

            List<TodoModel> todos = JsonConvert.DeserializeObject<List<TodoModel>>(json);

            return todos ?? new List<TodoModel>();
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            EnsureFile();

            List<TodoModel> todos = GetAll();
            return todos.Where(predicate).ToList();
        }

        public TodoModel GetById(int id)
        {
            EnsureFile();

            List<TodoModel> todos = GetAll();
            return todos.FirstOrDefault(t => t.Id == id);
        }

        public TodoModel Update(TodoModel todo)
        {
            EnsureFile();

            List<TodoModel> todos = GetAll();

            TodoModel existingTodo = todos.FirstOrDefault(t => t.Id == todo.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.Date = todo.Date;
                existingTodo.IsDone = todo.IsDone;
            }

            SaveAll(todos);

            return existingTodo;
        }

        private void SaveAll(List<TodoModel> todos)
        {
            string json = JsonConvert.SerializeObject(todos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(PATH, json);
        }
    }
}
