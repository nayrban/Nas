using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NasScheduleService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
     
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
            new ScheduleService()
            };
            ServiceBase.Run(ServicesToRun);            
        }

       /* static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                ScheduleService service1 = new ScheduleService();
                service1.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ScheduleService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }*/
    }
}
