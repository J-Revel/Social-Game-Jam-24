using UnityEngine;

[CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
public class ScoreConfig : ScriptableObject
{
    public int goodMealMultiplier;
    public int badMealMultiplier;
    public int snackMultiplier;
    public int targetMealCount;
}
