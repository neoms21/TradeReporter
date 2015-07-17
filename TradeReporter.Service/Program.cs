using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace TradeReporter.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //#if DEBUG
            //            //While debugging this section is used.
            //            var myService = new Service();
            //            myService.Start();
            //            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);




            // Please go to command prompt as administrator, migrate to Release Folder and run command - 'TradeReporter.Service.exe --install'

            if (Environment.UserInteractive)
            {
                string parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                        break;
                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                }
                Console.ReadLine();
            }
            else
            {
                var servicesToRun = new ServiceBase[] 
                          { 
                              new Service(),  
                          };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
