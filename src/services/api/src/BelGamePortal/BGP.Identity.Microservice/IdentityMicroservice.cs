using BGP.Identity.Microservice.Interface;
using BGP.Shared.Libs.Microservice;

namespace BGP.Identity.Microservice;

public class IdentityMicroservice(IServiceProvider serviceProvider) : BaseMicroservice(serviceProvider), IIdentityMicroservice
{

}
