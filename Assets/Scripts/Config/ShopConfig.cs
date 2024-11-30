using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Scriptable Objects/ShopConfig")]
public class ShopConfig : ScriptableObject
{
    public ProductConfig[] AvaillableProducts;
    public int MaxProductCount;
}
