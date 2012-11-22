SignalRWindsorFacility adds ability to call client side services just with injected interface

## Example

1. Define contract for client-side service

    ```csharp
    public interface IChat
    {
        void AddMessage(string msg);
    }
    ```

2. Register `IChat` as client-side service associated with `ChatHub`

    ```csharp
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<ClientServiceFacility>();

            container.Register(
                Component.For<IChat>().AsClientService().WithHub<ChatHub>());
        }
    }
    ```

3. Write your client-side logic

    ```js
    (function ($) {
        $.extend($.connection.chatHub.client, {
            addMessage: function(msg) {
                alert(msg);
            }
        });
        $.connection.hub.start();
    })(jQuery);  
    ```

4. Inject `IChat` anywhere and call its methods as normal

    ```csharp
    public class HomeController : Controller
    {
        readonly IChat chat;

        public HomeController(IChat chat)
        {
            this.chat = chat;
        }

        public ActionResult Index()
        {
            chat.AddMessage("Hello");

            return View();
        }
    }
    ```