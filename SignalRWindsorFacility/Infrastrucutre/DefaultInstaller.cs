using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SignalRWindsorFacility.Controllers;
using SignalRWindsorFacility.Hubs;
using SignalRWindsorFacilityCore;

namespace SignalRWindsorFacility.Infrastrucutre
{
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<ClientServiceFacility>();

            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<IController>()
                                      .LifestyleTransient());

            container.Register(
                Component.For<IChat>().AsClientService().WithHub<ChatHub>());
        }
    }
}