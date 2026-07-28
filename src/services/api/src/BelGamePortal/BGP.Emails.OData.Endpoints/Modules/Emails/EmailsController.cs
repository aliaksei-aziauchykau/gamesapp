using BGP.Emails.Domain.Entities;
using BGP.Emails.Microservice.Interfaces;
using BGP.Emails.OData.Endpoints.Modules.Emails;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Emails.OData.Api.Modules.Emails;

public class EmailsController : CrudODataControllerExtendedBase<Email, Guid, EmailCreateDto, EmailUpdateDto, EmailPatchDto>
{
    public EmailsController(IEmailsMicroservice serviceProvider) : base(serviceProvider)
    {
    }
}
