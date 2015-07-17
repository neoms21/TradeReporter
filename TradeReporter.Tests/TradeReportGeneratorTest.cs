using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services;
using TradeReporter.Interfaces;

namespace TradeReporter.Tests
{
    [TestFixture]
    public class TradeReportGeneratorTest
    {
        private TradeReportGenerator _generator;
        private Mock<ILogger> _mockLogger;
        private Mock<ITradeReportWriter> _mockWriter;
        private Mock<ITradeAggregator> _mockAggregator;
        private Mock<IPowerService> _mockPowerService;


        [SetUp]
        public void Setup()
        {
            _mockWriter = new Mock<ITradeReportWriter>();
            _mockAggregator = new Mock<ITradeAggregator>();
            _mockWriter = new Mock<ITradeReportWriter>();
            _mockLogger = new Mock<ILogger>();
            _mockPowerService = new Mock<IPowerService>();

            _generator = new TradeReportGenerator
            {
                Logger = _mockLogger.Object,
                TradeAggregator = _mockAggregator.Object,
                ReportWriter = _mockWriter.Object,
                PowerService = _mockPowerService.Object
            };
        }

        //TODO Moq has bug that if try to return from method then setup doesn't work. Investigate.
        //[Test]
        //public void GivenPowerServiceWhenTradesAreAskedTAndErrorIsThrownOnceThenProcessIsRetried()
        //{
        //    IEnumerable<PowerTrade> powerTrades = new List<PowerTrade>();
        //    var curDate = DateTime.Now.Date;
        //    var index = 0;

        //    _mockPowerService.Setup(p => p.GetTrades(curDate)).Returns<IEnumerable<PowerTrade>>(x=> { return GetTrades(index); });

        //    _generator.GenerateReport();
        //    _mockLogger.Verify(l => l.LogInfo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        //}

        //private IEnumerable<PowerTrade> GetTrades(int index)
        //{

        //    if (index++ == 0)
        //        throw new PowerServiceException("Trade Retreival failed");

        //    return new List<PowerTrade>();
        //}

        [Test]
        public void GivenPowerServiceWhenTradesAreAskedThenTradesAreReturnedAndWrittenToOutputFile()
        {
            var powerTrades = new List<PowerTrade>();
            var powerPeriods = new List<PowerPeriod>();
            _mockPowerService.Setup(p => p.GetTrades(It.IsAny<DateTime>())).Returns(powerTrades);

            _generator.GenerateReport();

            _mockLogger.Verify(l => l.LogInfo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(4));
            _mockAggregator.Verify(a => a.AggregateTrades(powerTrades), Times.Once);
            _mockWriter.Verify(a => a.WriteToFile(powerPeriods, It.IsAny<string>()), Times.Once);
        }
    }
}
