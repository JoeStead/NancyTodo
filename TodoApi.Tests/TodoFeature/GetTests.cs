using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Newtonsoft.Json;
using TodoApi.Todo;
using TodoApi.Todo.Infrastructure;
using Xunit;

namespace TodoApi.Tests.TodoFeature
{
    public class GetTests
    {

        [Fact]
        public async Task ShouldReturn200AndTodoItemListWhenSomeExist()
        {
            var items = new[]
            {
                new Todo.Todo("details", false),
                new Todo.Todo("Hello", false),
            };

            var expected = new[]
            {
                new Todo.Todo(1, "details", false),
                new Todo.Todo(2, "Hello", false),
            };

            var repository = new TodoRepository();
            foreach (var item in items)
            {
                repository.Create(item);
            }

            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(repository);
            });

            var response = await browser.Get("/todo", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<IEnumerable<Todo.Todo>>(response.Body.AsString());
            body.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturn200AndEmptyListWhenTodoItemDoesNotExist()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));
            });

            var response = await browser.Get("/todo", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<IEnumerable<Todo.Todo>>(response.Body.AsString());
            body.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldReturn200AndTodoItemWhenItExists()
        {
            var repository = new TodoRepository();
            repository.Create(new Todo.Todo("My Details", false));

            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(repository);
            });

            var response = await browser.Get("/todo/1", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<Todo.Todo>(response.Body.AsString());
            body.ShouldBeEquivalentTo(new Todo.Todo(1, "My Details", false));
        }

        [Fact]
        public async Task ShouldReturn404WhenTodoItemDoesNotExist()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));
            });

            var response = await browser.Get("/todo/1234", ctx =>
            {
                ctx.Accept(new MediaRange("application/json"));
            });

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}