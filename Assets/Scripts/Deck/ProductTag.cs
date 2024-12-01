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

[System.Serializable]
public struct ProductFilter
{
    public ProductFilterType filter_type;
    public ProductTag tag;

    public bool IsCompatibleWithTags(ProductTag[] tags)
    {
        foreach(ProductTag tag_element in tags)
        {
            if (tag_element == tag)
            {
                return filter_type == ProductFilterType.Require;
            }
        }
        return filter_type == ProductFilterType.Incompatible;
    }
}
