using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRWindsorFacilityCore
{
    class HubInterceptor<THub> : IInterceptor where THub : IHub
    {
        static readonly ICollection<string> ConnectionIdArgNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "connection",
                "connectionId",
                "connection_id",
                "client",
                "clientId",
                "client_id",
            };

        static readonly ICollection<string> GroupNameArgNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "group",
                "groupName",
                "group_name",
            };

        public void Intercept(IInvocation invocation)
        {
            object[] args;
            var clientProxy = GetClientProxy(invocation, out args);
            if (clientProxy != null)
                invocation.ReturnValue = clientProxy.Invoke(invocation.Method.Name, args);
        }

        private static IClientProxy GetClientProxy(IInvocation call, out object[] args)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<THub>();

            var methodParameters = call.Method.GetParameters();
            if (methodParameters.Length > 0)
            {
                var parameterInfo = methodParameters[0];
                var firstArg = call.Arguments[0].ToString();
                if (ConnectionIdArgNames.Contains(parameterInfo.Name))
                {
                    args = call.Arguments.Skip(1).ToArray();
                    return (IClientProxy) hub.Clients.Client(firstArg);
                }
                if (GroupNameArgNames.Contains(parameterInfo.Name))
                {
                    args = call.Arguments.Skip(1).ToArray();
                    return (IClientProxy) hub.Clients.Group(firstArg);
                }
            }

            args = call.Arguments;
            return (IClientProxy) hub.Clients.All;
        }
    }
}