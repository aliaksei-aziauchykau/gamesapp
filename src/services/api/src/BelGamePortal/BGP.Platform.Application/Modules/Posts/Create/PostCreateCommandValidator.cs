
using BGP.Platform.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using FluentValidation;

namespace BGP.Platform.Application.Modules.Posts;

public class PostCreateCommandValidator : AbstractValidator<AddEntityCommand<Post, Guid>>
{
    public PostCreateCommandValidator()
    {
        RuleFor(v => v.Entity.Content)
            .MaximumLength(PostValidationRules.ContentMaxLength);

        RuleFor(v => v.Entity.Title)
            .MaximumLength(PostValidationRules.TitleMaxLength)
            .NotEmpty();
    }
}
