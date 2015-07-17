using System;
using System.ServiceProcess;
using System.Threading;
using Autofac;
using TradeReporter.Interfaces;

namespace TradeReporter.Service
{
    public class Service : ServiceBase
    {
        private ILogger _logger;
        private bool _isRunning;
        private Thread _thread;
        private ITradeReportGenerator _reporter;

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TradeReporterModule());
            var container = builder.Build();
            _reporter = container.Resolve<ITradeReportGenerator>();
            _logger = container.Resolve<ILogger>();
            _logger.LogInfo("Service Started");


            _thread = new Thread(RunReport);
            _thread.Start();
        }

        protected override void OnStop()
        {
            _logger.LogInfo("Stopping Trade Report Service");
        }

        private void RunReport()
        {
            var interval = Convert.ToInt32(Settings.Default.ReportInterval);

            _isRunning = true;
            while (_isRunning)
            {
                _logger.LogInfo("Running Power Position Report ");

                try
                {
                    _reporter.GenerateReport();
                    Thread.Sleep(interval * 60 * 1000);
                }
                catch (Exception exception)
                {
                    _logger.LogException(exception);
                }
            }
        }

        public void Start()
        {
            OnStart(new string[0]);
        }

        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "TradeReportService";

        }
    }
}
