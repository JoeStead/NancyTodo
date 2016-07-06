using System;
using Newtonsoft.Json;

namespace TodoApi.Todo
{
    public class Todo
    {
        public Todo(string details, bool completed)
        {
            Details = details;
            Completed = completed;
        }

        [JsonConstructor]
        public Todo(int id, string details, bool completed)
        {
            Id = id;
            Details = details;
            Completed = completed;
        }

        public int Id { get; set; }
        public string Details { get; }
        public bool Completed { get; }
    }
}
