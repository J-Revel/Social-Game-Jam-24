using UnityEngine;

[CreateAssetMenu(fileName = "ProductTag", menuName = "Product/ProductTag")]
public class ProductTag : ScriptableObject
{
    
}

public enum ProductFilterType
{
    Require,
    Incompatible,
}

public struct ProductFilter
{
    public ProductFilterType filter_type;
    public ProductTag tag;
}
