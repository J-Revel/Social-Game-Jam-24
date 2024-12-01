using UnityEngine;

[CreateAssetMenu(fileName = "WeeklyEventConfig", menuName = "Scriptable Objects/WeeklyEventConfig")]
public class WeeklyEventConfig : ScriptableObject
{
    public string TitleKey;
    public string DescriptionKey;
    public string SuccessKey;
    public string FailedKey;
    public string FactKey;
    public string CitationKey;
}