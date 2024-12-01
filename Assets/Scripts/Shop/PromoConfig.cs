using UnityEngine;

[CreateAssetMenu(fileName = "PromoConfig", menuName = "Scriptable Objects/PromoConfig")]
public class PromoConfig : ScriptableObject
{
    public float price_multiplier = 0.5f;
    public Vector2Int promo_card_count_range;
    public float promo_card_probability;
    public string display_text;
}
