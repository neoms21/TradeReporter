using System.Collections.Generic;
using Services;

namespace TradeReporter.Interfaces
{
    public interface ITradeReportWriter
    {
        void WriteToFile(IEnumerable<PowerPeriod> powerPeriods, string filePath);
    }
}