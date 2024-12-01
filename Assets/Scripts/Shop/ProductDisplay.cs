using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductDisplay : MonoBehaviour
{
    public ProductConfig product_config;
    public TMP_Text title_text;
    public Image icon_image;
    public TMP_Text cost_text;
    

    void Start()
    {
        if(title_text != null)
            title_text.text = product_config.title;
        if(icon_image != null)
            icon_image.sprite = product_config.icon;
        if(cost_text != null)
            cost_text.text = (product_config.cost/100).ToString() + "." + (product_config.cost%100).ToString("00");
    }

    void Update()
    {
        
    }
}
