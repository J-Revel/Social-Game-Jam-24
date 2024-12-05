using UnityEngine;



[CreateAssetMenu(fileName = "ProductConfig", menuName = "Product/ProductConfig")]
public class ProductConfig : ScriptableObject
{
    public string title;
    public int cost;
    public string description;
    public Sprite icon;
    public ProductTag[] tags;
    public AlimentDescription[] aliments;
    public int timeCost = 1;
}
