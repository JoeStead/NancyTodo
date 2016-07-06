using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using TodoApi.Todo.Infrastructure;

namespace TodoApi.Todo
{
    public class TodoModule : NancyModule
    {
        private readonly ITodoRepository _todoRepository;

        public TodoModule(ITodoRepository todoRepository) : base("/todo")
        {
            _todoRepository = todoRepository;

            Get["/"] = GetAll;

            Get["/{id:int}"] = GetById;

            Post["/"] = Create;

            Put["/{id:int}"] = UpdateById;

            Delete["/{id:int}"] = DeleteById;
        }

        public Task<dynamic> GetAll(dynamic parameters, CancellationToken cancellationToken)
        {
            var todoItems = _todoRepository.GetAll();
            return Task.FromResult<dynamic>(Negotiate.WithModel(todoItems));
        }

        private Task<dynamic> GetById(dynamic parameters, CancellationToken cancellationToken)
        {
            try
            {
                var todoItem = _todoRepository.Get((int)parameters.id);
                return Task.FromResult<dynamic>(Negotiate.WithModel(todoItem));
            }
            catch (TodoItemNotFoundException)
            {
                return Task.FromResult<dynamic>(HttpStatusCode.NotFound);
            }
        }
        
        private Task<dynamic> Create(dynamic parameters, CancellationToken cancellationToken)
        {
            var body = this.Bind<CreateTodo>();
            var validationResult = this.Validate(body);

            if (!validationResult.IsValid)
            {
                return Task.FromResult<dynamic>(Negotiate.WithModel(validationResult).WithStatusCode(HttpStatusCode.BadRequest));
            }
            var id = _todoRepository.Create(new Todo(body.Details, false));
            var nancyRoot = Request.Url.SiteBase;

            return Task.FromResult<dynamic>(Negotiate.WithHeader("location", $"{nancyRoot}/todo/{id}").WithStatusCode(HttpStatusCode.Created));
        }

        private Task<dynamic> UpdateById(dynamic parameters, CancellationToken cancellationToken)
        {
            var body = this.Bind<UpdateTodo>();
            var validationResult = this.Validate(body);

            if (!validationResult.IsValid)
            {
                return Task.FromResult<dynamic>(Negotiate.WithModel(validationResult).WithStatusCode(HttpStatusCode.BadRequest));
            }

            try
            {
                _todoRepository.Update(new Todo((int)parameters.id, body.Details, body.Completed));
                return Task.FromResult<dynamic>(HttpStatusCode.NoContent);
            }
            catch (TodoItemNotFoundException)
            {
                return Task.FromResult<dynamic>(HttpStatusCode.NotFound);
            }
        }

        private Task<dynamic> DeleteById(dynamic parameters, CancellationToken cancellationToken)
        {
            try
            {
                _todoRepository.Delete((int)parameters.id);
                return Task.FromResult<dynamic>(HttpStatusCode.NoContent);
            }
            catch (TodoItemNotFoundException)
            {
                return Task.FromResult<dynamic>(HttpStatusCode.NotFound);
            }
        }
    }
}