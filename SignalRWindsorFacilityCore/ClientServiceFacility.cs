using Castle.DynamicProxy;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace SignalRWindsorFacilityCore
{
    public class ClientServiceFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(Component.For<StandardInterceptor>().OnlyNewServices());
        }
    }
}