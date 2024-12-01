public struct TransactionEventData
{
    public ProductConfig Product;
    public AlimentDescription[] TransactionDescription { get { return Product.aliments; } }
    public int TimeCost;
    public int PriceCost;
}
