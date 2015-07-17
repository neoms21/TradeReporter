using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Services;

namespace TradeReporter.Tests
{

    [TestFixture]
    public class TradeAggregatorTest
    {

        [Test]
        public void GivenVolumesToAggregateWhenAggregatedThenCorrectVolumesAreReturned()
        {

            var powerTrades = CreateTrades();
            var tradeAggregator = new TradeAggregator();

            var aggregateTrades = tradeAggregator.AggregateTrades(powerTrades).ToList();


            for (var i = 0; i < 24; i++)
            {
                Assert.AreEqual(i < 11 ? 150 : 80, aggregateTrades[i].Volume);
            }

        }

        private static IEnumerable<PowerTrade> CreateTrades()
        {
            IList<PowerTrade> powerTrades = new List<PowerTrade>();

            powerTrades.Add(CreateTrade(100));
            powerTrades.Add(CreateTrade(50, true));

            return powerTrades;
        }

        private static PowerTrade CreateTrade(double volume, bool toVary = false)
        {
            var powerTrade = PowerTrade.Create(DateTime.Now.Date, 24);

            for (int i = 0; i < 24; i++)
                powerTrade.Periods[i] = new PowerPeriod { Period = i + 1, Volume = toVary && i >= 11 ? volume - 70 : volume };

            return powerTrade;
        }
    }
}
