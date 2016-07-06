using System.Threading.Tasks;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using TodoApi.Todo;
using TodoApi.Todo.Infrastructure;
using Xunit;

namespace TodoApi.Tests.TodoFeature
{
    public class DeleteTests
    {
        [Fact]
        public async Task ShouldReturn204AndDeleteItemWhenItExists()
        {
            var repository = new TodoRepository();
            repository.Create(new Todo.Todo("Details", false));
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(repository);
            });

            var response = await browser.Delete("/todo/1");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            repository.GetAll().Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldReturn404WhenItemDoesNotExist()
        {
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
                with.Dependency<ITodoRepository>(typeof(TodoRepository));

            });

            var response = await browser.Delete("/todo/456");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}