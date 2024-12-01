using JetBrains.Annotations;
using System.Collections.Generic;

[System.Serializable]
public class ProductPromoConfig
{
    public PromoConfig config;
    public float probability_weight;
}

[System.Serializable]
public class ShopProductConfig
{
    public ProductConfig product;
    public ProductPromoConfig[] promos;
}

public class ShopContentElement
{
    public ProductConfig product;
    public PromoConfig promo;
}
public class ShopContentData
{
    public ProductConfig[] Products;
    public float coupon_gain_probability = 0.5f;
    public PlayingCardConfig[] coupons;
}