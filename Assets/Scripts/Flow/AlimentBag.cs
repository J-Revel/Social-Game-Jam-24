using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlimentBag: MonoBehaviour
{
    [SerializeField] private AlimentBagData[] _dataDictionnary;
    private Dictionary<AlimentType, AlimentBagData> dataDictionnary;
    public int Get(AlimentType type) => dataDictionnary[type].Amount;
    public void AddFromTransaction(TransactionEventData data)
    {
        foreach(AlimentDescription alimentDescription in data.TransactionDescription)
        {
            dataDictionnary[alimentDescription.Type].Amount += alimentDescription.Amount;
            //QueueAnimation(alimentDescription);
        }
    }

    private void Awake()
    {
        dataDictionnary = _dataDictionnary.ToDictionary(data => data.Type, data => data);
    }
}

[Serializable]
public class AlimentBagData
{
    public AlimentType Type;
    public IconCounter Counter;
    public int Amount;
}