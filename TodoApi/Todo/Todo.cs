using System;

namespace TodoApi.Todo
{
    public class Todo
    {
        public Todo(string details, bool completed)
        {
            Details = details;
            Completed = completed;
        }

        public string Details { get; }
        public bool Completed { get; }
    }
}
