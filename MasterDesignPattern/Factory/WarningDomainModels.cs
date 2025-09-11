namespace MasterDesignPattern.Factory
{
    // This is Value object because there is no identity 
    // Should be Immutable so that no one change the 'message' once created
    public record Warning(string Type, string Message, string Category);

    // These are Domain Model becuase they are related to specific domain 
    // These are also Immutable but have identity like 
    // Position  -> Security Id
    // Cash  -> Portfolio Code
    // Open Order -Security Id
    // Domain Model 

    // Make Immutable 
    public record PositionModel(int SecId, string SecName, decimal MarketValue, decimal FaceValue, decimal InteradayQty);
    public record CashProjectionModel(string PortfolioCode, string PortfolioName, string TransType, decimal CurrentCash);
    public record OpenOrderModel(int SecId, string SecName, string TransType, decimal OpenQty, decimal CloseQty);
}
