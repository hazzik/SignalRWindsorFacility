using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRWindsorFacilityCore
{
    public class ClientServiceRegistration<TClientService> where TClientService : class
    {
        readonly ComponentRegistration<TClientService> registration;

        internal ClientServiceRegistration(ComponentRegistration<TClientService> registration)
        {
            this.registration = registration;
        }

        public ComponentRegistration<TClientService> WithHub<THub>() where THub : IHub
        {
            registration.Interceptors<StandardInterceptor>().SelectInterceptorsWith(new HubInterceptorSelector<THub>());
            return registration;
        }
    }
}
