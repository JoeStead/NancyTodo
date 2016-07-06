using System.Collections.Generic;

namespace TodoApi.Todo.Infrastructure
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        Todo Get(int id);
        int Create(Todo item);
        void Update(Todo item);
        void Delete(int id);
    }
}
