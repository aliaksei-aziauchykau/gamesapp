using BGP.Shared.Libs.FormEntitySchemas.Interfaces;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
using MediatR;

namespace ITD.Core.Application.Commands.FormSchema;
public class GetEntityFormSchemaCommand<TEntity> : IRequest<IFormEntitySchema> where TEntity : class
{ }

public class GetEntityFormSchemaCommandHandler<TEntity> : IRequestHandler<GetEntityFormSchemaCommand<TEntity>, IFormEntitySchema> where TEntity : class
{

    private readonly IFormEntitySchemaConfiguration<TEntity> _formEntitySchemaProvider;

    public GetEntityFormSchemaCommandHandler(IFormEntitySchemaConfiguration<TEntity> formEntitySchemaProvider)
    {
        _formEntitySchemaProvider = formEntitySchemaProvider;
    }

    public async Task<IFormEntitySchema> Handle(GetEntityFormSchemaCommand<TEntity> command, CancellationToken cancellationToken)
    {
        var configuration = await _formEntitySchemaProvider.ConfigureAsync();

        return configuration.FormEntitySchema;
    }
}
