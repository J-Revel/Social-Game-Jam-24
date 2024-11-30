using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public ShopContentData products;
    public RectTransform product_container;
    public RectTransform popup_container;
    public ShopBuyButton buy_button_prefab;
    public DeckPanel deck_panel;
    void Start()
    {
        for(int i=0; i<products.Products.Length; i++)
        {
            ShopBuyButton button = Instantiate(buy_button_prefab, product_container);
            button.deckPanel = deck_panel;
            button.product = products.Products[i];
            button.popup_container = popup_container;
        }
    }

    void Update()
    {
        
    }
}
