using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Global : MonoBehaviour
{
    private static Global singleton;

    [Header("Configs")]
    [SerializeField] private WeeklyEventsConfig weeklyEventsConfig;
    [SerializeField] private ShopConfig[] shopConfigs;

    [Header("Prefab")]
    [SerializeField] private GameObject levelPrefab;
    private GameObject currentLevelPrefab = null;

    public static WeeklyEventsConfig WeeklyEventsConfig => singleton.weeklyEventsConfig;
    public static ShopConfig[] ShopConfigs => singleton.shopConfigs;

    private WeeklyEventConfig currentWeeklyEvent;
    private ShopContentData[] currentShopList;

    public static WeeklyEventConfig CurrentWeeklyEvent => singleton.currentWeeklyEvent;
    public static ShopContentData[] CurrentShopList => singleton.currentShopList;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        // Pick Next Event
        var potentialEvents = this.weeklyEventsConfig.AvaillableEvents.Where(e => e != this.currentWeeklyEvent).ToArray();
        this.currentWeeklyEvent = potentialEvents[Random.Range(0, potentialEvents.Length)];

        Reset();

        // Pick Shop Products for each shop
        for (int i = 0; i < this.shopConfigs.Length; i++)
        {
            ShopConfig shopConfig = this.shopConfigs[i];
            currentShopList[i] = new ShopContentData();
            currentShopList[i].Products = new ProductConfig[Mathf.Min(shopConfig.MaxProductCount, shopConfig.AvailableProducts.Length)];
            currentShopList[i].coupons = shopConfig.coupons;
            currentShopList[i].coupon_gain_probability = shopConfig.coupon_gain_probability;
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
                currentShopList[i].Products[p] = product; 
            }
        }

        this.currentLevelPrefab = GameObject.Instantiate(levelPrefab);
    }

    private void Reset()
    {
        this.currentShopList = new ShopContentData[this.shopConfigs.Length];
        if(this.currentLevelPrefab != null)
        {
            GameObject.Destroy(this.currentLevelPrefab);
            this.currentLevelPrefab = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}