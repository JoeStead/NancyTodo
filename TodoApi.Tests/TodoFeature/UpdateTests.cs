using System.Threading.Tasks;
using FluentAssertions;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using TodoApi.Todo;
using TodoApi.Todo.Infrastructure;
using Xunit;

namespace TodoApi.Tests.TodoFeature
{
    public class UpdateTests
    {
        [Fact]
        public async Task ShouldReturn204AndUpdateValidTodoItem()
        {
            var repository = new TodoRepository();
            repository.Create(new Todo.Todo("details", false));
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(repository);

            });

            var response = await browser.Put("/todo/1",
                ctx =>
                {
                    ctx.Accept(new MediaRange("application/json"));
                    ctx.JsonBody(new UpdateTodo
                    {
                        Details = "new details",
                        Completed = true
                    });
                });

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            repository.Get(1).Should().Match<Todo.Todo>(t => t.Details == "new details" && t.Completed);
        }

        [Fact]
        public async Task ShouldReturn400AndNotUpdateForInvalidTodoItem()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));

            });

            var response = await browser.Put("/todo/1",
                ctx =>
                {
                    ctx.Accept(new MediaRange("application/json"));
                    ctx.JsonBody(new UpdateTodo
                    {
                        Details = null,
                        Completed = true
                    });
                });

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldReturn404WhenItemDoesNotExist()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));

            });

            var response = await browser.Put("/todo/456",
                ctx =>
                {
                    ctx.Accept(new MediaRange("application/json"));
                    ctx.JsonBody(new UpdateTodo
                    {
                        Details = "details",
                        Completed = true
                    });
                });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
