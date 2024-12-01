using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayPart : GamePart
{
    [SerializeField] private BaseLevelData baseLevelData;
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
        this.InjectLevelResources();
        HelpManager.OnMapOpen();
        // TransactionManager.Set values ?
    }

    private void InjectLevelResources()
    {
        //Money Modifier
        int time = baseLevelData.StartingTime;
        int money = baseLevelData.StartingMoney + Global.CurrentWeeklyEvent.MoneyModifier;
        this.currentLevel.GetComponentInChildren<MoneyBag>().Init(time, money);

        // Promo Modifier
        int basePromoCoupon = baseLevelData.StartingCouponsCount + Global.CurrentWeeklyEvent.StartingCouponsModifier;
        // TODO apply

        // HelpProductCount Modifier
        ShopConfig HelpShopConfig = Global.ShopConfigs[2];
        HelpShopConfig.MaxProductCount = baseLevelData.HelpProductCount + Global.CurrentWeeklyEvent.HelpProductCountModifier;

        // Meal Modifier
        int mealNeededCount = baseLevelData.MealNeeded + Global.CurrentWeeklyEvent.MealNeededModifier;
        //this.currentLevel.GetComponentInChildren<ScoreManager>().Init(mealNeededCount);

        // Help Access Modifier
        this.currentLevel.GetComponentInChildren<HelpAccesserTag>().gameObject.SetActive(Global.CurrentWeeklyEvent.HelpAvaillable);
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
                /*
                float promo_random_value = Random.Range(0, 1.0f);
                float promo_weight_sum = 0;
                for(int j=0; j<product.promos.Length; j++)
                {
                    promo_weight_sum += product.promos[j].probability_weight;
                }
                int promo_index = 0;
                for(int j=0; j<product.promos.Length; j++)
                {
                    promo_random_value -= product.promos[j].probability_weight / promo_weight_sum;
                    if (promo_random_value < 0.0f)
                    {
                        promo_index = j;
                        break;
                    }
                }
                */
                newShopContent[i].Products[p] = product;
            }
        }

        Global.CurrentShopList = newShopContent;
    }
}