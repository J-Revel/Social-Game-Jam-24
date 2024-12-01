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
    public void PayForTransaction(TransactionEventData data)
    {
        dataDictionnary[MoneyType.Money].Amount -= data.PriceCost;
        //TODO QueueAnimation
        dataDictionnary[MoneyType.Money].Counter.Set(dataDictionnary[MoneyType.Money].Amount);
        dataDictionnary[MoneyType.Time].Amount -= data.TimeCost;
        //TODO QueueAnimation
        dataDictionnary[MoneyType.Time].Counter.Set(dataDictionnary[MoneyType.Time].Amount);
    }

    private void Start()
    {
        //Set value in counter
        foreach(MoneyType moneyType in dataDictionnary.Keys)
        {
            dataDictionnary[moneyType].Counter.Set(dataDictionnary[moneyType].Amount);
        }
    }
}

[Serializable]
public class MoneyBagData
{
    public MoneyType Type;
    public IconCounter Counter;
    public int Amount;
}