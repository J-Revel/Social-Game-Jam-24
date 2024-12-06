using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton { get; private set;}
    [SerializeField] public Gauge gauge;
    [SerializeField] public ScoreConfig scoreConfig;
    private Result currentScore = new();
    private int currentMealTarget;

    public static Result GetScore() => singleton.currentScore;

    public static void OnTransactionUpdate()
    {
        singleton.ComputeScore();
        singleton.gauge.Value = singleton.currentScore.QuestProgression;
        singleton.gauge.Update();
    }

    public void ComputeScore()
    {
        // Get values
        int proteinCount = TransactionManager.singleton.alimentBag.Get(AlimentType.Protein);
        int vegetableCount = TransactionManager.singleton.alimentBag.Get(AlimentType.Vegetable);
        int starchCount = TransactionManager.singleton.alimentBag.Get(AlimentType.Starch);
        int snackCount = TransactionManager.singleton.alimentBag.Get(AlimentType.Snacks);
        
        // Compute Result
        int goodMealCount = Mathf.Min(proteinCount,vegetableCount,starchCount);
        int badMealCount = Mathf.CeilToInt(((float)(proteinCount + vegetableCount + starchCount - goodMealCount*3))/3);
        int totalMealCount = goodMealCount + badMealCount;
        float questProgression = Mathf.Clamp01((float)totalMealCount / (float)this.currentMealTarget);
        int mealsMissed = Mathf.Max(0, currentMealTarget - totalMealCount);
        int score = goodMealCount * scoreConfig.goodMealMultiplier 
                    + badMealCount * scoreConfig.badMealMultiplier
                    + snackCount * scoreConfig.snackMultiplier;

        this.currentScore = new Result()
        {
            Score = score,
            GoodMealCount = goodMealCount,
            BadMealCount = badMealCount,
            TotalMealCount = totalMealCount,
            MissedMealCount = mealsMissed,
            QuestProgression = questProgression,
        };
    }

    public struct Result
    {
        public int GoodMealCount;
        public int BadMealCount;
        public int TotalMealCount;
        public int MissedMealCount;
        public int Score;
        public float QuestProgression;
    }

    public static void Init(int mealTarget)
    {
        singleton.currentMealTarget = mealTarget;
        singleton.ComputeScore();
    }
    private void Awake() {singleton = this;}
}
