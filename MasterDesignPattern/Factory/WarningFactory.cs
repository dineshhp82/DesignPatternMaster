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
                new PositionModel (1, "ABC",  1000, 1000,  -10 ),
                new PositionModel (2, "XYZ",  -500, 500,5 )
            ];
        }

        private IEnumerable<CashProjectionModel> GetCashProjectionModels()
        {
            return
            [
                new CashProjectionModel ( "P1", "Portfolio 1",  "Buy", 500 ),
                new CashProjectionModel ( "P2", "Portfolio 2",  "Sell", 800 )
            ];
        }

        private IEnumerable<OpenOrderModel> GetOpenOrders()
        {
            return
            [
               new OpenOrderModel (  1,  "ABC", "Buy", -10, 0 ),
               new OpenOrderModel (  2,  "XYZ", "Sell", 5,  -5 )
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
