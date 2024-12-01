using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public ShopContentData products;
    public RectTransform product_container;
    public RectTransform popup_container;
    public ShopBuyButton buy_button_prefab;
    public DeckPanel deck_panel;
    public CardBonusPopup card_bonus_popup;
    void Start()
    {
        for(int i=0; i<products.Products.Length; i++)
        {
            ShopBuyButton button = Instantiate(buy_button_prefab, product_container);
            button.transaction_delegate += (transaction) =>
            {
                TransactionManager.RegisterTransaction(transaction);
                if (Random.Range(0, 1.0f) < products.coupon_gain_probability)
                {
                    CardBonusPopup card_bonus = Instantiate(card_bonus_popup, transform);
                    card_bonus.card_received = products.coupons[Random.Range(0, products.coupons.Length)];
                    card_bonus.deck_panel = deck_panel;
                }
            };
            button.deckPanel = deck_panel;
            button.product = products.Products[i];
            button.popup_container = popup_container;
        }
    }

    void Update()
    {
        
    }
}
