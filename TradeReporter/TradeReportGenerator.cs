using System;
using System.Collections.Generic;
using System.IO;
using Services;
using TradeReporter.Interfaces;

namespace TradeReporter
{
    public class TradeReportGenerator : ITradeReportGenerator
    {

        public void GenerateReport()
        {
            IEnumerable<PowerTrade> trades = null;

            do
            {
                try
                {
                    Logger.LogInfo("Powerservice trade retrieval started", this.GetType().Name, "GenerateReport");
                    trades = PowerService.GetTrades(DateTime.Now.Date);
                    Logger.LogInfo("Trades Retreived");
                }
                catch (PowerServiceException powerServiceException)
                {
                    Logger.LogException(powerServiceException);
                }
            }
            while (trades == null);

            var powerPeriods = TradeAggregator.AggregateTrades(trades);
            Logger.LogInfo("Aggregation Complete");
            var filePath = GetFilePath();
            Logger.LogInfo("Volumes being written to Filepath" + filePath);
            ReportWriter.WriteToFile(powerPeriods, filePath);
        }

        private static string GetFilePath()
        {
            var path = Settings1.Default.OutputDirectory;

            return Path.Combine(path, string.Format("PowerPosition_{0}.csv", DateTime.Now.ToString("yyyyMMdd_HHmm")));
        }

        public ITradeAggregator TradeAggregator { get; set; }
        public ITradeReportWriter ReportWriter { get; set; }
        public IPowerService PowerService { get; set; }
        public ILogger Logger { get; set; }

    }
}