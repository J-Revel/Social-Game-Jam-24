using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MonoBehaviour
{
    private Button button;
    public Image image;
    public TMP_Text text;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            
        });
    }

    void Update()
    {
        
    }
}
