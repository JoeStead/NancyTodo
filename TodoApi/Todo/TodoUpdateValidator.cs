using FluentValidation;

namespace TodoApi.Todo
{
    public class TodoUpdateValidator : AbstractValidator<UpdateTodo>
    {
        public TodoUpdateValidator()
        {
            RuleFor(t => t.Details).NotEmpty();
        }
    }
}
