using UnityEngine;

[CreateAssetMenu(fileName = "BaseLevelData", menuName = "Scriptable Objects/BaseLevelData")]
public class BaseLevelData : ScriptableObject
{
    public int StartingTime = 30;
    public int StartingMoney = 10000;
    public int StartingCouponsCount = 5;
    public int MealNeeded = 21;
    public int HelpProductCount = 3;
}
