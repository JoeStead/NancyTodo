using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Todo.Infrastructure
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        Todo Get(int id);
        void Create(Todo item);
        void Update(Todo item);
        void Delete(int id);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly List<Todo> _todoDatabaseSource;

        public TodoRepository()
        {
            _todoDatabaseSource = new List<Todo>();
        }

        public IEnumerable<Todo> GetAll()
        {
            return _todoDatabaseSource;
        }

        public Todo Get(int id)
        {
            if (_todoDatabaseSource.Count < id)
            {
                throw new TodoItemNotFoundException();
            }

            return _todoDatabaseSource.ElementAt(id - 1);
        }

        public void Create(Todo item)
        {
            _todoDatabaseSource.Add(item);
        }

        public void Update(Todo item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }

    public class TodoItemNotFoundException : Exception
    {

    }
}
