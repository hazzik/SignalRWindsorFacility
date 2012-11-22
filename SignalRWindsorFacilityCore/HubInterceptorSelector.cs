using System;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRWindsorFacilityCore
{
    class HubInterceptorSelector<THub> : IInterceptorSelector where THub : IHub
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return new IInterceptor[]
                {
                    new HubInterceptor<THub>()
                };
        }
    }
}