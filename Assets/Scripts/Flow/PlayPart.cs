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
            newShopContent[i].Products = new ShopContentElement[shopConfig.MaxProductCount];
            for (int p = 0; p < shopConfig.MaxProductCount; p++)
            {
                int productPickIndex = UnityEngine.Random.Range(0, shopConfig.AvailableProducts.Length);
                ShopProductConfig product = shopConfig.AvailableProducts[productPickIndex];
                //product.product
                float promo_random_value = UnityEngine.Random.Range(0, 1.0f);
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

                newShopContent[i].Products[p] = new ShopContentElement
                {
                    product = product.product,
                    promo = product.promos[promo_index].config,
                };
            }
        }

        Global.CurrentShopList = newShopContent;
    }
}
