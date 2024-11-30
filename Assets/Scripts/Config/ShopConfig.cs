using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Scriptable Objects/ShopConfig")]
public class ShopConfig : ScriptableObject
{
    public ShopProductConfig[] AvailableProducts;
    public int MaxProductCount;
}
