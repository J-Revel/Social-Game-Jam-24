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
    public System.Action<TransactionEventData> buy_delegate;

    int price { get { return Mathf.RoundToInt(product.cost * deckPanel.selected_price_multiplier); } }
            
    void Start()
    {
        product_icon.sprite = product.icon;
        confirm_button.onClick.AddListener(() =>
        {
            popup_close_delegate?.Invoke();
            Destroy(gameObject);
            deckPanel.ConsumeSelectedCards();
            buy_delegate?.Invoke(new TransactionEventData
            {
                PriceCost = price,
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
        price_display.text = (price / 100).ToString() + "," + (price % 100).ToString("00");
        confirm_button.interactable = price <= TransactionManager.singleton.moneyBag.Get(MoneyType.Money);
    }
}
