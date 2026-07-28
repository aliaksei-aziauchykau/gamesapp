using BGP.Shared.Libs.Microservice;
using BGP.Auth.Microservice.Interfaces;

namespace BGP.Auth.Microservice;

public class AuthMicroservice(IServiceProvider serviceProvider) : BaseMicroservice(serviceProvider), IAuthMicroservice
{
}
