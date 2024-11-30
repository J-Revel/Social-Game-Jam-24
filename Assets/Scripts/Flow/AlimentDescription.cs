using UnityEngine;

[CreateAssetMenu(fileName = "AlimentDescription", menuName = "ScriptableObjects/AlimentDescription")]
public class AlimentDescription : ScriptableObject
{
    public ShopType ShopType;
    public string Name;
    public AlimentType Type;
    public int Amount;
}
