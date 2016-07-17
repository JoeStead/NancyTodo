using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using TodoApi.Todo;
using Xunit;

namespace TodoApi.Tests.TodoFeature
{
    public class CreateValidatorTests
    {
        private readonly TodoCreateValidator _subject;

        public CreateValidatorTests()
        {
            _subject = new TodoCreateValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldHaveValidationFailuresWhenDetailsIsEmpty(string value)
        {
            _subject.ShouldHaveValidationErrorFor(f => f.Details, value);
        }

        [Fact]
        public void ShouldNotHaveValidationFailuresForValidItem()
        {
            var result = _subject.Validate(new CreateTodo
            {
                Details = "Details!"
            });
            result.IsValid.Should().BeTrue();
        }
    }
}
