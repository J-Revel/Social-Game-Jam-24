using System.Collections.Generic;
using UnityEngine;

public class PlayPart : GamePart
{
    [SerializeField] private GameObject levelPrefab;
    private GameObject currentLevel = null;
    public override void EndPart()
    {
        if(this.currentLevel != null)
        {
            GameObject.Destroy(this.currentLevel);
            this.currentLevel = null;
        }
    }

    public override void StartPart()
    {
        GenerateLevelData();
        this.currentLevel = GameObject.Instantiate(levelPrefab);
        HelpManager.OnMapOpen();
        // TransactionManager.Set values ?
    }

    private void GenerateLevelData()
    {
        ShopContentData[] newShopContent = new ShopContentData[Global.ShopConfigs.Length];
        // Pick Shop Products for each shop
        for (int i = 0; i < Global.ShopConfigs.Length; i++)
        {
            ShopConfig shopConfig = Global.ShopConfigs[i];
            newShopContent[i] = new ShopContentData();
            newShopContent[i].Products = new ProductConfig[Mathf.Min(shopConfig.MaxProductCount, shopConfig.AvailableProducts.Length)];
            newShopContent[i].coupons = shopConfig.coupons;
            newShopContent[i].coupon_gain_probability = shopConfig.coupon_gain_probability;
            List<ProductConfig> unused_products = new List<ProductConfig>(shopConfig.AvailableProducts);
            for (int p = 0; p < shopConfig.MaxProductCount && unused_products.Count > 0; p++)
            {
                int productPickIndex = Random.Range(0, unused_products.Count);
                ProductConfig product = unused_products[productPickIndex];
                unused_products.RemoveAt(productPickIndex);
                newShopContent[i].Products[p] = product;
            }
        }

        Global.CurrentShopList = newShopContent;
    }
}
