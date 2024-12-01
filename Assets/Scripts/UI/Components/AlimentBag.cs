using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
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
            dataDictionnary[alimentDescription.Type].Counter.Set(dataDictionnary[alimentDescription.Type].Amount);
        }
    }

    private void Awake()
    {
        dataDictionnary = _dataDictionnary.ToDictionary(data => data.Type, data => data);
    }

    private void Start()
    {
        //Set value in counter
        foreach(AlimentType alimentType in dataDictionnary.Keys)
        {
            dataDictionnary[alimentType].Counter.Set(dataDictionnary[alimentType].Amount);
        }
    }
}

[Serializable]
public class AlimentBagData
{
    public AlimentType Type;
    public IconCounter Counter;
    public int Amount;
}