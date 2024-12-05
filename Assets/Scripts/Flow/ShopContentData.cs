using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

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
    public ShopConfig config;
    public List<ProductConfig> Products;
}