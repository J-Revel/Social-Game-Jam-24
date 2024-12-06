using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyPopup : MonoBehaviour
{
    public DeckPanel deckPanel;
    public TMP_Text price_display;
    public Button confirm_button;
    public Button cancel_button;
    public System.Action popup_close_delegate;
    public System.Action<TransactionEventData> buy_delegate;
    private ProductDisplay product_display;

    int price { get { return Mathf.RoundToInt(product_display.product_config.cost * deckPanel.selected_price_multiplier); } }
            
    void Start()
    {
        deckPanel = DeckPanel.singleton;
        product_display = GetComponent<ProductDisplay>();
        confirm_button.onClick.AddListener(() =>
        {
            int paidPrice = price; // Cache it before cleanin selected_card;
            popup_close_delegate?.Invoke();
            Destroy(gameObject);
            deckPanel.ConsumeSelectedCards();
            buy_delegate?.Invoke(new TransactionEventData
            {
                PriceCost = paidPrice,
                Product = product_display.product_config,
                TimeCost = product_display.product_config.timeCost,
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
