using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class EndPanel : GamePart
{
    [Header("ScoreArea")]
    [SerializeField] private ScoreConfig scoreConfig;
    [SerializeField] private ResultLine[] alimentResultLines;
    [SerializeField] private Image line0;
    [SerializeField] private ResultLine goodResultLine;
    [SerializeField] private ResultLine badResultLine;
    [SerializeField] private Image line1;
    [SerializeField] private ResultLine scoreResultLine;

    [Header("EventArea")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI factText;
    [SerializeField] private TextMeshProUGUI citationText;

    public override void EndPart()
    {
        this.gameObject.SetActive(false);
    }   

    public override void StartPart()
    {
        UpdateVisual();
        this.gameObject.SetActive(true);
    }

    public void UpdateVisual()
    {
        // Fill aliment lines
        foreach(int alimentIndex in Enum.GetValues(typeof(AlimentType)))
        {
            AlimentType type = (AlimentType)alimentIndex;
            int amount = TransactionManager.singleton.alimentBag.Get(type);
            alimentResultLines[alimentIndex].SetResult(amount);
        }

                // ComputeScore
        List<int> goodAlimentAmounts = new List<int>();
        int snackAmount = 0;

        foreach(int alimentIndex in Enum.GetValues(typeof(AlimentType)))
        {
            AlimentType type = (AlimentType)alimentIndex;
            int amount = TransactionManager.singleton.alimentBag.Get(type);
            alimentResultLines[alimentIndex].SetResult(amount);
            if(type != AlimentType.Snacks)
            {
                goodAlimentAmounts.Add(amount);
            }
            else
            {
                snackAmount = amount;
            }
        }

        int goodMealCount = goodAlimentAmounts.Min();
        int badMealCount = Mathf.CeilToInt(((float)(goodAlimentAmounts.Sum() - goodMealCount*3))/3);
        int mealsMissed = scoreConfig.targetMealCount - goodMealCount - badMealCount;
        int score = goodMealCount * scoreConfig.goodMealMultiplier 
                    + badMealCount * scoreConfig.badMealMultiplier
                    + snackAmount * scoreConfig.snackMultiplier;
        
        goodResultLine.SetResult(goodMealCount);
        badResultLine.SetResult(badMealCount);
        scoreResultLine.SetResult(score);

        var currentEvent = Global.CurrentWeeklyEvent;
        titleText.text = currentEvent.TitleLocKey.GetLocalizedString();
        resultText.text = mealsMissed <= 0 ? currentEvent.SuccessLocKey.GetLocalizedString() : String.Format(currentEvent.FailedLocKey.GetLocalizedString(), mealsMissed);
        PedagogicTextCouple pickedTextCouple = currentEvent.PedagogicTexts[UnityEngine.Random.Range(0, currentEvent.PedagogicTexts.Length)];
        citationText.text = pickedTextCouple.CitationLocKey.GetLocalizedString();
        factText.text = pickedTextCouple.FactLocKey.GetLocalizedString();
    }

    public void Next()
    {
        Global.GoToState(GameState.Presentation);
    }  
}
