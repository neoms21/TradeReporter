using System.Collections.Generic;
using Services;

namespace TradeReporter.Interfaces
{
    public interface ITradeAggregator
    {
        IEnumerable<PowerPeriod> AggregateTrades(IEnumerable<PowerTrade> powerTrades);
    }
}