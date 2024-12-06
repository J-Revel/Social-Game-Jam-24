using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public ShopContentData products;
    public RectTransform product_container;
    public RectTransform popup_container;
    public ShopBuyButton buy_button_prefab;
    public CardBonusPopup card_bonus_popup;
    public Image background_image;

    void Start()
    {
        background_image.sprite = products.config.background_sprite;
        for(int i=0; i<products.Products.Count; i++)
        {
            ProductConfig productConfig = products.Products[i];
            ShopBuyButton button = Instantiate(buy_button_prefab, product_container);
            button.GetComponent<ProductDisplay>().product_config = productConfig;
            button.transaction_delegate += (transaction) =>
            {
                TransactionManager.RegisterTransaction(transaction);
                if (Random.Range(0, 1.0f) < products.config.coupon_gain_probability)
                {
                    CardBonusPopup card_bonus = Instantiate(card_bonus_popup, transform);
                    card_bonus.card_received = products.config.coupons[Random.Range(0, products.config.coupons.Length)];
                }
                if (products.config.CanBuyOnlyOnce)
                {
                    products.Products.Remove(productConfig);
                    Destroy(button.gameObject);
                }
            };
            button.popup_container = popup_container;
        }
    }
}
