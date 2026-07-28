using AutoMapper;
using MediatR;

namespace BGP.Shared.Modules.AutoMapper.Commands;

public class MapCommand<TSource, TDestination> : IRequest<TDestination>
{
    public MapCommand(TSource source)
    {
        Source = source;
    }

    public TSource Source { get; set; }
}

public class MapCommandHandler<TSource, TDestination> : IRequestHandler<MapCommand<TSource, TDestination>, TDestination>
{
    private readonly IMapper _mapper;

    public MapCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<TDestination> Handle(MapCommand<TSource, TDestination> command, CancellationToken cancellationToken)
    {
        var destination = _mapper.Map<TDestination>(command.Source);

        return Task.FromResult(destination);
    }
}
