using System;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private GameObject mapOverlayPanel;
    public int currentShopIndex = -1;
    public ShopData[] shops;
    public ShopMenu shop_prefab;
    private ShopMenu displayed_shop;
    public Transform shop_menu_container;

    public void ToggleMap()
    {
        mapOverlayPanel.SetActive(!mapOverlayPanel.activeSelf);
    }

    public void GoToShop(int shopIndex)
    {
        if(currentShopIndex != shopIndex)
        {
            if(currentShopIndex != -1)
            {
                // Hide last shop
                if (displayed_shop != null)
                    Destroy(displayed_shop);
            }

            // Activate next shop
            currentShopIndex = shopIndex;
            displayed_shop = Instantiate(shop_prefab, shop_menu_container);
            displayed_shop.products = Global.CurrentShopList[currentShopIndex];
        }
    }
}

[Serializable]
public class ShopData
{
    public GameObject Shop;
}
