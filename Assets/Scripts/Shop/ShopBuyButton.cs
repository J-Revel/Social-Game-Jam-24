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
    
    void Start()
    {
        button = GetComponent<Button>();
        foreach(Image img in images)
        {
            img.sprite = product.icon;
        }
        button.onClick.AddListener(() =>
        {
            deckPanel.StartCardSelection(product.tags);
            ShopBuyPopup popup = Instantiate(buy_popup_prefab, transform.position, Quaternion.identity, popup_container);
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
