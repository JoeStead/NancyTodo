using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
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

        [Fact]
        public void ShouldHaveValidationFailuresWhenDetailsIsEmpty()
        {
            var result = _subject.Validate(new UpdateTodo
            {
                Details = string.Empty
            });
            result.IsValid.Should().BeFalse();
            result.Errors.First().Should().Match<ValidationFailure>(e => e.PropertyName == nameof(CreateTodo.Details));
        }

        [Fact]
        public void ShouldHaveValidationFailuresWhenDetailsIsNull()
        {
            var result = _subject.Validate(new UpdateTodo());
            result.IsValid.Should().BeFalse();
            result.Errors.First().Should().Match<ValidationFailure>(e => e.PropertyName == nameof(CreateTodo.Details));
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

