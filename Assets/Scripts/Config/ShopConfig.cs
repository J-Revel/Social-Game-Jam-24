using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Scriptable Objects/ShopConfig")]
public class ShopConfig : ScriptableObject
{
    public ProductConfig[] AvailableProducts;
    public int MaxProductCount;

    public float coupon_gain_probability = 0.5f;
    public PlayingCardConfig[] coupons;
}
