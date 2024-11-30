using System.Collections.Generic;
using UnityEngine;

public class AlimentBag: MonoBehaviour
{
    private Dictionary<AlimentType, int> bank = new Dictionary<AlimentType,int>();
    public int Get(AlimentType type) => bank[type];
    public void AddFromTransaction(TransactionEventData data)
    {
        foreach(AlimentDescription alimentDescription in data.TransactionDescription)
        {
            bank[alimentDescription.Type] += alimentDescription.Amount;
            //QueueAnimation(alimentDescription);
        }
    }
}
