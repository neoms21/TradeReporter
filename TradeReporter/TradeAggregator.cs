using System.Collections.Generic;
using System.Linq;
using Services;
using TradeReporter.Interfaces;

namespace TradeReporter
{
    public class TradeAggregator : ITradeAggregator
    {
        public IEnumerable<PowerPeriod> AggregateTrades(IEnumerable<PowerTrade> powerTrades)
        {

            var periods = powerTrades.SelectMany(x => x.Periods);

            var results = periods.GroupBy(x => x.Period)
                .Select(y => new { y.Key, VolumePeriod = new PowerPeriod { Period = y.Key, Volume = y.Sum(t => t.Volume) } });

            return results.Select(x => x.VolumePeriod);
        }
    }
}