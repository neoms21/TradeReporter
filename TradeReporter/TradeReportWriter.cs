using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Services;
using TradeReporter.Interfaces;

namespace TradeReporter
{
    public class TradeReportWriter : ITradeReportWriter
    {
        private const string ColumnHeader = "Local Time,Volume";

        public void WriteToFile(IEnumerable<PowerPeriod> powerPeriods, string filePath)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(ColumnHeader);

                var sortedVolumes = powerPeriods.OrderBy(it => it.Period).ToList();

                foreach (var aggregatedVolume in sortedVolumes)
                    streamWriter.WriteLine("{0},{1}", DateTime.Today.AddHours(aggregatedVolume.Period - 2).ToString("HH:mm"), aggregatedVolume.Volume);
            }
        }
    }
}