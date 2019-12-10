using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web.Http;
//using ConsoleApp;

namespace Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            Main();
        }

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern int AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole();
            //Program program = new Program();
            Console.WriteLine("Hi 2");
            //Console.ReadLine();
            //Console.ReadKey();
        }
    }
}
