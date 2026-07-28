using AutoMapper;
using MediatR;

namespace BGP.Shared.Modules.AutoMapper.Commands;

public class PatchCommand<TSource, TDestination> : IRequest<TDestination>
{
    public PatchCommand(TSource source, TDestination destination)
    {
        Source = source;
        Destination = destination;
    }

    public TSource Source { get; set; }

    public TDestination Destination { get; set; }
}

public class PatchCommandHandler<TSource, TDestination> : IRequestHandler<PatchCommand<TSource, TDestination>, TDestination>
{
    private readonly IMapper _mapper;

    public PatchCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<TDestination> Handle(PatchCommand<TSource, TDestination> command, CancellationToken cancellationToken)
    {
        var destination = _mapper.Map(command.Source, command.Destination);

        return Task.FromResult(destination);
    }
}
