using System.Threading.Tasks;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using TodoApi.Todo;
using Xunit;

namespace TodoApi.Tests.TodoFeature
{
    public class DeleteTests
    {
        [Fact]
        public async Task ShouldReturn200AndDeleteItemWhenItExists()
        {
            var deleteId = 123;
            var browser = new Browser(with =>
            {
                with.Module<TodoModule>();
            });

            var response = await browser.Delete($"/todo/{deleteId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //todo: Item should be deleted from the repository.
        }

        [Fact]
        public async Task ShouldReturn404WhenItemDoesNotExist()
        {
            var browser = new Browser(with =>
                {
                    with.Module<TodoModule>();
                });

            var response = await browser.Delete("/todo/456");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}