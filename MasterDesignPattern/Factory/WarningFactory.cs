namespace MasterDesignPattern.Factory
{
    internal class WarningFactory
    {
        public void Simulator()
        {
            Console.WriteLine("-------------Warning Factory----------------");
            var positionFactory = new PositionWarningFactory(GetPositionModels());
            var positionWarnings = positionFactory.CreateWarning();

            var cashFactory = new CashWarningFactory(GetCashProjectionModels());
            var cashWarnings = cashFactory.CreateWarning();

            var openOrderFactory = new OpenOrderWarningFactory(GetOpenOrders());
            var openOrderWarnings = openOrderFactory.CreateWarning();

            var allWarnings = positionFactory.GetWarnings()
                .Concat(cashFactory.GetWarnings())
                .Concat(openOrderFactory.GetWarnings());

            foreach (var item in allWarnings)
            {
                Console.WriteLine($"{item.Category} {item.Message} {item.Category}");
            }

        }

        private IEnumerable<PositionModel> GetPositionModels()
        {
            return
            [
                new PositionModel { SecId = 1, SecName = "ABC", MarketValue = 1000, FaceValue = 1000, InteradayQty = -10 },
                new PositionModel { SecId = 2, SecName = "XYZ", MarketValue = -500, FaceValue = 500, InteradayQty = 5 }
            ];
        }

        private IEnumerable<CashProjectionModel> GetCashProjectionModels()
        {
            return
            [
                new CashProjectionModel { PortfolioCode = "P1", PortfolioName = "Portfolio 1", TransType = "Buy", CurrentCash = 500 },
                new CashProjectionModel { PortfolioCode = "P2", PortfolioName = "Portfolio 2", TransType = "Sell", CurrentCash = 800 }
            ];
        }

        private IEnumerable<OpenOrderModel> GetOpenOrders()
        {
            return
            [
               new OpenOrderModel { SecId = 1, SecName = "ABC", TransType = "Buy", OpenQty = -10, CloseQty = 0 },
               new OpenOrderModel { SecId = 2, SecName = "XYZ", TransType = "Sell", OpenQty = 5, CloseQty = -5 }
            ];
        }

    }

    /*
          Factory Method Pattern

           Warning - Value Object class Immutable 
            -Type
            -Message
            -Category

           WarningBase 
              IEnumerable<WarningBase> GetWarnings();

           PositionWarning: WarningBase
              IEnumerable<WarningBase> GetWarnings();

           CashWarning: WarningBase
              IEnumerable<WarningBase> GetWarnings();

           OpenOrderWarning: WarningBase
              IEnumerable<WarningBase> GetWarnings();   

          WarningFactory
           abstract WarningBase CreateWarning();
           IEnumerable<WarningBase> GetWarnings()
           { 
            var warning=CreateWarning();
            waring.GetWarning();
           }

          PositionWarningCreator: WarningFactory
             override CreateWarning() => new PositionsWarning();

           CashWarningCreator: WarningFactory
             override CreateWarning() => new CashWarning();

          OpenOrderWarningCreator: WarningFactory
             override CreateWarning() => new OpenOrdersWarning();
         */

    // This is Value object because there is no identity 
    // Should be Immutable so that no one change the 'message' once created
    public record Warning(string Type, string Message, string Category);

    // These are Domain Model becuase they are related to specific domain 
    // These are also Immutable but have identity like 
    // Position  -> Security Id
    // Cash  -> Portfolio Code
    // Open Order -Security Id
    // Domain Model 

    public class PositionModel
    {
        public int SecId { get; set; }
        public string SecName { get; set; }
        public decimal MarketValue { get; set; }
        public decimal FaceValue { get; set; }
        public decimal InteradayQty { get; set; }
    }
    public class CashProjectionModel
    {
        public string PortfolioCode { get; set; }
        public string PortfolioName { get; set; }
        public string TransType { get; set; }
        public decimal CurrentCash { get; set; }
    }
    public class OpenOrderModel
    {
        public int SecId { get; set; }
        public string SecName { get; set; }
        public string TransType { get; set; }
        public decimal OpenQty { get; set; }
        public decimal CloseQty { get; set; }
    }


    /// <summary>
    /// Base class for different types of warnings.
    /// </summary>
    public abstract class WarningBase
    {
        public abstract IEnumerable<Warning> GetWarnings();
    }

    public class CashProjectionWarning : WarningBase
    {
        private readonly IEnumerable<CashProjectionModel> cashProjections;

        public CashProjectionWarning(IEnumerable<CashProjectionModel> cashProjections)
        {
            this.cashProjections = cashProjections;
        }

        public override IEnumerable<Warning> GetWarnings()
        {
            if (cashProjections.Any(x => x.CurrentCash < 0))
            {
                yield return new Warning("CashWarning", "Negative cash balance detected", "Cash");
            }

            if (cashProjections.Any(x => x.TransType == "Sell" && x.CurrentCash < 1000))
            {
                yield return new Warning("CashWarning", "Low cash balance for Sell", "Cash");
            }
        }
    }

    public class PositionWarning : WarningBase
    {
        private readonly IEnumerable<PositionModel> positions;

        public PositionWarning(IEnumerable<PositionModel> positions)
        {
            this.positions = positions;
        }

        public override IEnumerable<Warning> GetWarnings()
        {
            if (positions.Any(x => x.InteradayQty < 0))
            {
                yield return new Warning("PositionWarning", "Negative interaday quantity detected", "Position");
            }

            if (positions.Any(x => x.MarketValue < 0))
            {
                yield return new Warning("PositionWarning", "Negative market value detected", "Position");
            }

        }
    }

    public class OpenOrderWarning : WarningBase
    {
        private readonly IEnumerable<OpenOrderModel> orderModels;

        public OpenOrderWarning(IEnumerable<OpenOrderModel> orderModels)
        {
            this.orderModels = orderModels;
        }
        public override IEnumerable<Warning> GetWarnings()
        {
            if (orderModels.Any(x => x.OpenQty < 0))
            {
                yield return new Warning("OpenOrderWarning", "Negative open quantity detected", "OpenOrder");
            }

            if (orderModels.Any(x => x.CloseQty < 0))
            {
                yield return new Warning("OpenOrderWarning", "Negative close quantity detected", "OpenOrder");
            }
        }
    }


    public abstract class WarningFactoryBase
    {
        public abstract WarningBase CreateWarning();

        public IEnumerable<Warning> GetWarnings()
        {
            var warning = CreateWarning();
            return warning.GetWarnings();
        }
    }


    public class PositionWarningFactory : WarningFactoryBase
    {
        private readonly IEnumerable<PositionModel> positions;

        public PositionWarningFactory(IEnumerable<PositionModel> positions)
        {
            this.positions = positions;
        }
        public override WarningBase CreateWarning() => new PositionWarning(positions);


    }

    public class CashWarningFactory : WarningFactoryBase
    {
        private readonly IEnumerable<CashProjectionModel> cashProjections;
        public CashWarningFactory(IEnumerable<CashProjectionModel> cashProjections)
        {
            this.cashProjections = cashProjections;
        }

        public override WarningBase CreateWarning() => new CashProjectionWarning(cashProjections);
    }

    public class OpenOrderWarningFactory : WarningFactoryBase
    {
        private readonly IEnumerable<OpenOrderModel> openOrders;
        public OpenOrderWarningFactory(IEnumerable<OpenOrderModel> openOrders)
        {
            this.openOrders = openOrders;
        }

        public override WarningBase CreateWarning() => new OpenOrderWarning(openOrders);
    }
}
