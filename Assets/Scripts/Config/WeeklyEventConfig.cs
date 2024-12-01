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

    [Header("Modifiers")]
    public int MoneyModifier = 0;
    public bool HelpAvaillable = false;
    public int StartingCouponsModifier = 0;
    public int HelpProductCountModifier = 0;
    public int MealNeededModifier = 0;
}

[Serializable]
public class PedagogicTextCouple
{
    public LocalizedString FactLocKey;
    public LocalizedString CitationLocKey;
}