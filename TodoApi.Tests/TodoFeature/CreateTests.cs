using System.Linq;
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
    public class CreateTests
    {
        [Fact]
        public async Task ShouldReturn201AndSaveValidTodoItem()
        {
            var repository = new TodoRepository();

            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(repository);
            });

            var response = await browser.Post("/todo", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
                ctx.JsonBody(new CreateTodo
                {
                    Details = "details"
                });
            });

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var items = repository.GetAll();
            items.Single().Should().Match<Todo.Todo>(d => d.Completed == false && d.Details == "details");
        }

        [Fact]
        public async Task ShouldReturnLocationOfNewTodoItem()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));
            });

            var response = await browser.Post("/todo", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
                ctx.JsonBody(new CreateTodo
                {
                    Details = "details"
                });
            });

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers["Location"].Should().Be("http:///todo/1");
        }

        [Fact]
        public async Task ShouldReturn400ForInvalidTodoItem()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));
            });

            var response = await browser.Post("/todo", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
                ctx.JsonBody(new CreateTodo());
            });

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}