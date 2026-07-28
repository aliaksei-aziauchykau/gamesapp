using BGP.Shared.Libs.ServiceBus;

namespace BGP.Games.Integration.Events;

public class GameFetchedIntegrationEvent : IntegrationEvent
{
    public Guid GameId { get; set; }

    public string GameCode { get; set; }
}
