using System.Collections.Generic;
using UnityEngine;

public struct TransactionEventData
{
    public List<ScriptableObject> tags;
    public List<AlimentDescription> TransactionDescription;
    public int TimeCost;
    public int PriceCost;
}
