using FluentAssertions;
using FluentValidation.TestHelper;
using TodoApi.Todo;
using Xunit;

namespace TodoApi.Tests.TodoFeature
{
    public class UpdateValidatorTests
    {
        private readonly TodoUpdateValidator _subject;

        public UpdateValidatorTests()
        {
            _subject = new TodoUpdateValidator();
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
            var result = _subject.Validate(new UpdateTodo
            {
                Details = "Details!",
                Completed = true
            });
            result.IsValid.Should().BeTrue();
        }

    }
}

