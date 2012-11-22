using System.Threading;
using System.Web.Mvc;

namespace SignalRWindsorFacility.Controllers
{
    public interface IChat
    {
        void AddMessage(string msg);
    }

    public class HomeController : Controller
    {
        readonly IChat chat;

        public HomeController(IChat chat)
        {
            this.chat = chat;
        }

        public ActionResult Index()
        {
            new Thread(() =>
                {
                    Thread.Sleep(1000);
                    chat.AddMessage("Hello");
                }).Start();

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
