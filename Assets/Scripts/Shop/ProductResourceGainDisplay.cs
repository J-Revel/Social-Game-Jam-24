using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct AlimentTypeIcon
{
    public AlimentType type;
    public Sprite icon;
}

public class ProductResourceGainDisplay : MonoBehaviour
{
    public Image icon_image;
    public TMP_Text gain_text;
    public int gain_index = 0;
    public AlimentTypeIcon[] aliment_icons;
    

    void Start()
    {
        AlimentDescription to_display = GetComponentInParent<ProductDisplay>().product_config.aliments[gain_index];
        for(int i=0; i<aliment_icons.Length; i++)
        {
            if (aliment_icons[i].type == to_display.Type)
                icon_image.sprite = aliment_icons[i].icon;
        }
        gain_text.text = "+" + to_display.Amount.ToString();
    }
}
