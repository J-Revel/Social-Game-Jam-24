using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MonoBehaviour
{
    private Button button;
    public Image image;
    public TMP_Text text;
    public ProductConfig product;
    public DeckPanel deckPanel;
    

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            deckPanel.StartCardSelection(product.tags);
            
        });
    }

    void Update()
    {
        
    }
}
