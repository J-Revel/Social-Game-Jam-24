using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    [SerializeField] private MoneyBagData[] _dataDictionnary;
    private Dictionary<MoneyType, MoneyBagData> dataDictionnary;
    public int Get(MoneyType type) => dataDictionnary[type].Amount;
    private void Awake()
    {
        dataDictionnary = _dataDictionnary.ToDictionary(data => data.Type, data => data);
    }
    public int Money;
    public int Time;
    public void PayForTransaction(TransactionEventData data)
    {
        Money -= data.PriceCost;
        //QueueAnimation(alimentDescription);
        Time -= data.TimeCost;
        //QueueAnimation(alimentDescription);
    }
}

[Serializable]
public class MoneyBagData
{
    public MoneyType Type;
    public IconCounter Counter;
    public int Amount;
}