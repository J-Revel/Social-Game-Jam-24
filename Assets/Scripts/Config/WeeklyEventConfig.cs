using System;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "WeeklyEventConfig", menuName = "Scriptable Objects/WeeklyEventConfig")]
public class WeeklyEventConfig : ScriptableObject
{
    public LocalizedString TitleLocKey;
    public LocalizedString DescriptionLocKey;
    public LocalizedString SuccessLocKey;
    public LocalizedString FailedLocKey;
    public PedagogicTextCouple[] PedagogicTexts;
}

[Serializable]
public class PedagogicTextCouple
{
    public LocalizedString FactLocKey;
    public LocalizedString CitationLocKey;
}