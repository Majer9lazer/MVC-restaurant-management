using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using RestaurantManagement.Web.Models;

namespace RestaurantManagement.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            _logger.Info("1. Запуск приложения");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            //При закрытии приложения
            _logger.Info("2. Закрытие приложения");
        }

        protected void Application_BeginRequest()
        {
            //При начатом Запросе
            _logger.Info("3. Начало запроса");
        }
        protected void Application_EndRequest()
        {
            //При Оконченном Запросе
            _logger.Info("4 .Окончание запроса");
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //RouteData routeData = Context.Handler is MvcHandler handler ? handler.RequestContext.RouteData : null;
            //// var routeCulture = routeData != null ? routeData.Values["culture"].ToString() : null;
            //HttpCookie languageCookie = HttpContext.Current.Request.Cookies["lang"];
            //string[] userLanguages = HttpContext.Current.Request.UserLanguages;

            //// Set the Culture based on a route, a cookie or the browser settings,
            //// or default value if something went wrong
            //CultureInfo cultureInfo = new CultureInfo(languageCookie != null ? languageCookie.Value : "ru");

            //Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

            //CultureInfo culture = new CultureInfo("en-GB");
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat = culture.DateTimeFormat;
            //Thread.CurrentThread.CurrentCulture.NumberFormat = culture.NumberFormat;

            //switch (Thread.CurrentThread.CurrentCulture.Name)
            //{
            //    case "en-US":
            //        GlobalVariables.GlobalUserLanguages = "En";
            //        break;
            //    case "ru-RU":
            //        GlobalVariables.GlobalUserLanguages = "Ru";
            //        break;
            //    case "kk-KZ":
            //        GlobalVariables.GlobalUserLanguages = "Kk";
            //        break;
            //    default:
            //        GlobalVariables.GlobalUserLanguages = "";
            //        break;
            //}
        }
    }
}
