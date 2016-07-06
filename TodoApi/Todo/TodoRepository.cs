using System.Collections.Generic;
using TodoApi.Todo.Infrastructure;

namespace TodoApi.Todo
{
    public class TodoRepository : ITodoRepository
    {
        private readonly Dictionary<int, Todo> _items;
        private int _count;

        public TodoRepository()
        {
            _items = new Dictionary<int, Todo>();
            _count = 0;
        }

        public IEnumerable<Todo> GetAll()
        {
            return _items.Values;
        }

        public Todo Get(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new TodoItemNotFoundException(id);
            }

            return _items[id];
        }

        public int Create(Todo item)
        {
            _count++;
            item.Id = _count;
            _items.Add(_count, item);

            return _count; //This really wouldn't work in the real world, it's just asking for a race condition.
        }


        public void Update(Todo item)
        {
            if (!_items.ContainsKey(item.Id))
            {
                throw new TodoItemNotFoundException(item.Id);
            }
            _items[item.Id] = item;
        }

        public void Delete(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new TodoItemNotFoundException(id);
            }
            _items.Remove(id);
        }
    }
}