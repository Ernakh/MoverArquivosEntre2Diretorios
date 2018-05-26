using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MoverArquivosEntre2Diretorios
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
#if DEBUG   // debugando como DEBUG
                Service1 service = new Service1();
                service.StartDebug(new string[2]);
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else   // debugando como Release 
       ServiceBase[] ServicesToRun;
       ServicesToRun = new ServiceBase[] { new MeuServicoWindows() };
       ServiceBase.Run(ServicesToRun);
#endif
            }
            else   // codigo original
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Service1() };
                ServiceBase.Run(ServicesToRun);
            }
        }

        //ServiceBase[] ServicesToRun;
        //    ServicesToRun = new ServiceBase[]
        //    {
        //        new Service1()
        //    };
        //    ServiceBase.Run(ServicesToRun);
    }
}
