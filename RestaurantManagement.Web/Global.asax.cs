using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

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
    }
}
