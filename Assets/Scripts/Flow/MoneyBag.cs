using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public int Money;
    public int Time;
    public void PayForTransaction(TransactionEventData data)
    {
        Money -= data.PriceCost;
        Time -= data.TimeCost;
        //QueueAnimation(alimentDescription);
    }
}
