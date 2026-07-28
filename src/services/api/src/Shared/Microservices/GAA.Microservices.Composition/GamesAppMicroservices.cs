using GAA.Microservices.Auth;
using GAA.Microservices.Emails;
using GAA.Microservices.Games;
using GAA.Microservices.Identity;
using GAA.Microservices.Platform;
using GAA.Shared.Microservices.Abstractions;

namespace GAA.Microservices.Composition;

internal static class GamesAppMicroservices
{
    internal static readonly IReadOnlyList<IGamesAppMicroservice> All =
    [
        new GamesMicroserviceModule(),
        new AuthMicroserviceModule(),
        new EmailsMicroserviceModule(),
        new IdentityMicroserviceModule(),
        new PlatformMicroserviceModule()
    ];
}
