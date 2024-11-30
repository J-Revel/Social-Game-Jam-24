using System;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private GameObject mapOverlayPanel;
    public int currentShopIndex = -1;
    public ShopData[] shops;

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
                shops[currentShopIndex].Shop.SetActive(false);
            }

            // Activate next shop
            currentShopIndex = shopIndex;
            shops[currentShopIndex].Shop.SetActive(true);
        }
    }
}

[Serializable]
public class ShopData
{
    public GameObject Shop;
}
