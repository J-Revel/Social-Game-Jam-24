using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeeklyEventConfig", menuName = "Scriptable Objects/WeeklyEventConfig")]
public class WeeklyEventConfig : ScriptableObject
{
    [TextArea]public string TitleText;
    [TextArea]public string DescriptionText;
    [TextArea]public string SuccessText;
    [TextArea]public string FailedText;
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
    [TextArea] public string FactText;
    [TextArea] public string CitationText;
}