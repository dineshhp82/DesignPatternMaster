namespace MasterDesginPattern.TemplateMethod
{
    internal class BrokerFileParser
    {
        public BrokerFileParser()
        {
            //Achive runtime polymorphism swap behaviour at run time
            var jpMorganIsda = new JpMorganISDA();
            var brokersIsda = jpMorganIsda.GetBaseRecords().ToArray();

            var jpMorganIcsa = new JpMorganICSA();
            var brokersIcsa = jpMorganIcsa.GetBaseRecords().ToArray();
        }
    }
    /*
     
    We have different Broker say JpMorgan, Morgan Stanely ,BNP,BoA etc. And each have different firm legal entities
    say ISDA,ICSA,ICORE ect. Now each broker should return parsed base records for calculation and flow looks like.

     -- JpMorganBaseParser
         abstract  -> FirmLegalEntity
         abstract  -> FilePath
      
         Decimal GetMarketValue(){} --same for all entity
         abstract Decimal GetInvestedCash() 
        Template method  -> List GetBaseRecords()
            - ReadData(filePath)
            - decimal mv= getmarketValue();
            - decimal InvestedCash = GetInvsetedCash();

          -JpMorganISDAParser : JpMorganBaseParser
          -JpMorganICSAParser : JpMorganBaseParser
          -JpMorganICOREParser  : JpMorganBaseParser
     
     */

    public class BaseRecord
    {
        public string FirmLegalEntity { get; set; }

        public decimal MarketValue { get; set; }

        public decimal InvestedCash { get; set; }

        public decimal FxRate { get; set; }
    }

    //Abstraction
    public abstract class JpMorganAbstract
    {
        //Dynamic step
        public abstract string FirmLegalEnity { get; }

        //Dynamic step
        public abstract string FilePath { get; }

        //Template Method  - Method follow SRP
        //Encapsulation : Hide the internal Details
        public IEnumerable<BaseRecord> GetBaseRecords()
        {
            var investedCash = GetInvestedCash();
            Console.WriteLine($"{FirmLegalEnity} {investedCash}");
            yield return new BaseRecord
            {
                FirmLegalEntity = FirmLegalEnity,
                FxRate = 100,
                InvestedCash = investedCash,
                //Befor calc Market value we need to calc invested cash so order are defined
                MarketValue = GetMarketValue(investedCash)
            };
        }
        //Fixed Step
        decimal GetMarketValue(decimal investedCash)
        {
            return 100 * investedCash;
        }
        //Dynamic step
        public abstract decimal GetInvestedCash();
    }

    //Inhertaince,Polymorphism
    public class JpMorganISDA : JpMorganAbstract
    {
        public override string FirmLegalEnity => "ISDA";

        public override string FilePath => "C\\:TEMP";

        public override decimal GetInvestedCash()
        {
            return 123;
        }
    }

    //Inhertaince,Polymorphism
    public class JpMorganICSA : JpMorganAbstract
    {
        public override string FirmLegalEnity => "ICSA";

        public override string FilePath => "C\\:TEMP";

        public override decimal GetInvestedCash()
        {
            return 129;
        }
    }
}
