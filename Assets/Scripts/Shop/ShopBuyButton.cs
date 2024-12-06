using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MonoBehaviour
{
    private ProductDisplay product_display;
    private Button button;
    public DeckPanel deckPanel;
    public ShopBuyPopup buy_popup_prefab;
    public RectTransform popup_container;
    public System.Action<TransactionEventData> transaction_delegate;
    
    void Start()
    {
        deckPanel = DeckPanel.singleton;
        product_display = GetComponent<ProductDisplay>();
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            deckPanel.StartCardSelection(product_display.product_config.tags);
            ShopBuyPopup popup = Instantiate(buy_popup_prefab, transform.position, Quaternion.identity, popup_container);
            popup.buy_delegate += (transaction) =>
            {
                transaction_delegate?.Invoke(transaction);
            };
            popup.popup_close_delegate += () =>
            {
                deckPanel.StopCardSelection();
            };
            popup.GetComponent<ProductDisplay>().product_config = product_display.product_config;
        });
    }
}
