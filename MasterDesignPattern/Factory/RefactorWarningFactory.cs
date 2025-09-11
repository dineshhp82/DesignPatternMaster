namespace MasterDesignPattern.Factory
{
    internal class RefactorWarningFactory
    {
        public void Simulator()
        {
            Console.WriteLine("--------Refactor Warning----------");
            var position = new RefPositionFactory();
            var positionWarnings = position.GetWarnings(GetPositionModels());

            var cash = new RefCashProjectionFactory();
            var cashWarnings = cash.GetWarnings(GetCashProjectionModels());

            var openOrder = new RefOpenOrdersFactory();
            var openOrderWarnings = openOrder.GetWarnings(GetOpenOrders());

            var allWarnings = positionWarnings.Concat(cashWarnings).Concat(openOrderWarnings);

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

    public interface IWarningFactory<T>
    {
        IEnumerable<Warning> GetWarnings(IEnumerable<T> models);
    }

    public class RefPositionWarning : IWarningFactory<PositionModel>
    {
        public IEnumerable<Warning> GetWarnings(IEnumerable<PositionModel> positions)
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

    public class RefCashWarning : IWarningFactory<CashProjectionModel>
    {
        public IEnumerable<Warning> GetWarnings(IEnumerable<CashProjectionModel> cashProjections)
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

    public class RefOpenOrderWarning : IWarningFactory<OpenOrderModel>
    {
        public IEnumerable<Warning> GetWarnings(IEnumerable<OpenOrderModel> models)
        {
            if (models.Any(x => x.OpenQty < 0))
            {
                yield return new Warning("OpenOrderWarning", "Negative open quantity detected", "OpenOrder");
            }

            if (models.Any(x => x.CloseQty < 0))
            {
                yield return new Warning("OpenOrderWarning", "Negative close quantity detected", "OpenOrder");
            }
        }
    }

    public abstract class RefWarningFactoryBase<T>
    {
        protected abstract IWarningFactory<T> CreateWarningFactory();

        public IEnumerable<Warning> GetWarnings(IEnumerable<T> models)
        {
            if (models == null) throw new ArgumentNullException(nameof(models));
            var warningFactory = CreateWarningFactory();
            return warningFactory.GetWarnings(models);
        }
    }

    public class RefPositionFactory : RefWarningFactoryBase<PositionModel>
    {
        protected override IWarningFactory<PositionModel> CreateWarningFactory()
        {
            return new RefPositionWarning();
        }
    }

    public class RefCashProjectionFactory : RefWarningFactoryBase<CashProjectionModel>
    {
        protected override IWarningFactory<CashProjectionModel> CreateWarningFactory()
        {
            return new RefCashWarning();
        }
    }

    public class RefOpenOrdersFactory : RefWarningFactoryBase<OpenOrderModel>
    {
        protected override IWarningFactory<OpenOrderModel> CreateWarningFactory()
        {
            return new RefOpenOrderWarning();
        }
    }
}
