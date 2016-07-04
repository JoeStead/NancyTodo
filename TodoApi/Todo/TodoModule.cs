using System.Threading.Tasks;
using Nancy;
using TodoApi.Todo.Infrastructure;

namespace TodoApi.Todo
{
  public class TodoModule : NancyModule
  {
      public TodoModule(ITodoRepository todoRepository) : base("/todo")
      {
          Get["/"] = (_, ct) => Task.FromResult<dynamic>(Negotiate.WithModel(todoRepository.GetAll()));

          Get["/{id:int}"] = (p, ct) =>
          {
              try
              {
                  return Task.FromResult<dynamic>(Negotiate.WithModel(todoRepository.Get((int) p.id)));
              }
              catch (TodoItemNotFoundException)
              {
                  //log the error as a warning or some stuff
                  return Task.FromResult<dynamic>(HttpStatusCode.NotFound);
              }
          };
      }
  }
}