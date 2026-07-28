using AutoMapper;
using MediatR;

namespace BGP.Shared.Modules.AutoMapper.Commands;

public class MapListCommand<TSource, TDestination> : IRequest<IList<TDestination>>
{
    public MapListCommand(IList<TSource> sources)
    {
        Sources = sources;
    }

    public IList<TSource> Sources { get; set; }
}

public class MapListCommandHandler<TSource, TDestination> : IRequestHandler<MapListCommand<TSource, TDestination>, IList<TDestination>>
{
    private readonly IMapper _mapper;

    public MapListCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<IList<TDestination>> Handle(MapListCommand<TSource, TDestination> command, CancellationToken cancellationToken)
    {
        IList<TDestination> destinations = command.Sources.Select(source => _mapper.Map<TDestination>(source)).ToList();

        return Task.FromResult(destinations);
    }
}
