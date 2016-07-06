using System;

namespace TodoApi.Todo.Infrastructure
{
    public class TodoItemNotFoundException : Exception
    {
        public TodoItemNotFoundException(int id) : base($"Item with Id {id} could not be found")
        {
            
        }
    }
}