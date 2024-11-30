using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyPopup : MonoBehaviour
{
    public Image product_icon;
    public DeckPanel deckPanel;
    public ProductConfig product;
    public TMP_Text price_display;
    public Button confirm_button;
    public Button cancel_button;
    public System.Action popup_close_delegate;

    void Start()
    {
        product_icon.sprite = product.icon;
        confirm_button.onClick.AddListener(() =>
        {
            popup_close_delegate?.Invoke();
            Destroy(gameObject);
            int price_cents = Mathf.RoundToInt(product.cost * deckPanel.selected_price_multiplier);
            deckPanel.ConsumeSelectedCards();
            TransactionManager.RegisterTransaction(new TransactionEventData
            {
                PriceCost = price_cents,
                Product = product,
            });
        });
        cancel_button.onClick.AddListener(() => {
            popup_close_delegate?.Invoke();
            Destroy(gameObject); 
        });
    }

    void Update()
    {
        int price_cents = Mathf.RoundToInt(product.cost * deckPanel.selected_price_multiplier);
        price_display.text = (price_cents / 100).ToString() + "," + (price_cents % 100).ToString("00");
    }
}
