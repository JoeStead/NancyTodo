using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
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

        [Fact]
        public void ShouldHaveValidationFailuresWhenDetailsIsEmpty()
        {
            var result = _subject.Validate(new CreateTodo
            {
                Details = string.Empty
            });
            result.IsValid.Should().BeFalse();
            result.Errors.First().Should().Match<ValidationFailure>(e => e.PropertyName == nameof(CreateTodo.Details));
        }

        [Fact]
        public void ShouldHaveValidationFailuresWhenDetailsIsNull()
        {
            var result = _subject.Validate(new CreateTodo());
            result.IsValid.Should().BeFalse();
            result.Errors.First().Should().Match<ValidationFailure>(e => e.PropertyName == nameof(CreateTodo.Details));
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
