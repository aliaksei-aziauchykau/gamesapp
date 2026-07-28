using BGP.Shared.Modules.GameLogic.Kroko.Commands;
using FluentValidation;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameCommands.GuessWord;

public class KrokoGuessWordGameCommandValidator : AbstractValidator<KrokoGuessWordGameCommand>
{
    public KrokoGuessWordGameCommandValidator()
    {
        RuleFor(v => v.UserId)
            .Custom((UserId, context) =>
            {
                if (UserId == Guid.Empty.ToString())
                {
                    context.AddFailure("Id", "Id is required");
                }
            });
    }
}
