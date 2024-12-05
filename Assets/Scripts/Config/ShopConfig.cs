using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Scriptable Objects/ShopConfig")]
public class ShopConfig : ScriptableObject
{
    public ShopMenu shopPrefab;
    public Sprite background_sprite;
    public ProductConfig[] AvailableProducts;
    public int MaxProductCount;
    public bool CanBuyOnlyOnce = false;
    public float coupon_gain_probability = 0.5f;
    public PlayingCardConfig[] coupons;
}
