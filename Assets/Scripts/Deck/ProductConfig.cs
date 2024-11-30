using UnityEngine;



[CreateAssetMenu(fileName = "ProductConfig", menuName = "Scriptable Objects/ProductConfig")]
public class ProductConfig : ScriptableObject
{
    public string title;
    public int cost;
    public string description;
    public Sprite icon;
    public ProductTag[] tags;
    public AlimentDescription[] aliments;
}
