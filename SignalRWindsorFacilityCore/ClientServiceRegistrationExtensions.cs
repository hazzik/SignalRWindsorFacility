using Castle.MicroKernel.Registration;

namespace SignalRWindsorFacilityCore
{
    public static class ClientServiceRegistrationExtensions
    {
        public static ClientServiceRegistration<TClientService> AsClientService<TClientService>(this ComponentRegistration<TClientService> registration) 
            where TClientService : class
        {
            return new ClientServiceRegistration<TClientService>(registration);
        }
    }
}                                       