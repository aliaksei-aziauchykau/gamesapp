using MediatR;

namespace BGP.Platform.Application.Modules.Posts;

public static class PostValidationRules
{
    public const int TitleMaxLength = 200;

    public const int ContentMaxLength = 5000;

    public const int ViewComponentMaxLength = 100;
}
