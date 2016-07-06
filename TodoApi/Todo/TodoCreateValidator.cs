using FluentValidation;

namespace TodoApi.Todo
{
    public class TodoCreateValidator : AbstractValidator<CreateTodo>
    {
        public TodoCreateValidator()
        {
            RuleFor(t => t.Details).NotEmpty();
        }
    }
}
