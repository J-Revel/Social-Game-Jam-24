using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MonoBehaviour
{
    private Button button;
    public Image[] images;
    public TMP_Text text;
    public ProductConfig product;
    public DeckPanel deckPanel;
    public ShopBuyPopup buy_popup_prefab;
    public RectTransform popup_container;
    public TMP_Text price_text;
    public System.Action<TransactionEventData> transaction_delegate;
    
    void Start()
    {
        button = GetComponent<Button>();
        foreach(Image img in images)
        {
            img.sprite = product.icon;
        }
        int cost = Mathf.RoundToInt(product.cost);

        price_text.text = (cost/100).ToString() + "€" + (cost%100).ToString("00");
        button.onClick.AddListener(() =>
        {
            deckPanel.StartCardSelection(product.tags);
            ShopBuyPopup popup = Instantiate(buy_popup_prefab, transform.position, Quaternion.identity, popup_container);
            popup.buy_delegate += (transaction) =>
            {
                transaction_delegate?.Invoke(transaction);
            };
            popup.popup_close_delegate += () =>
            {
                deckPanel.StopCardSelection();
            };
            popup.deckPanel = deckPanel;
            popup.product = product;
        });
    }

    void Update()
    {
    }
}
