using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ImageServer
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //http[s]://server/[prefix/]identifier/region/size/rotation/quality[.format]

            //id1/full/full/0/native
            //id1/0,10,100,200/pct:50/90/native.png
            routes.MapRoute(
                "ImageTile",
                "images/{id}/{region}/{size}/{rotation}/{colorformat}",
                new { controller = "Image", action = "GetImageTile", id = "" }
            );

            //id1/info.json
            routes.MapRoute(
                "Info",
                "images/{id}/info.json",
                new { controller = "Image", action = "Info", id = "" }
            );


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}