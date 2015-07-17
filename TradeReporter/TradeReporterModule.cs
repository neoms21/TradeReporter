using Autofac;
using Services;
using TradeReporter.Interfaces;

namespace TradeReporter
{
    public class TradeReporterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => new TradeReportGenerator()).As<ITradeReportGenerator>().PropertiesAutowired().OwnedByLifetimeScope();
            builder.Register(c => new TradeReportWriter()).As<ITradeReportWriter>().PropertiesAutowired().OwnedByLifetimeScope();
            builder.Register(c => new TradeAggregator()).As<ITradeAggregator>().PropertiesAutowired().OwnedByLifetimeScope();
            builder.Register(c => new PowerService()).As<IPowerService>().PropertiesAutowired().OwnedByLifetimeScope();
            builder.Register(c => new Logger()).As<ILogger>().SingleInstance().PropertiesAutowired();
        }
    }
}
