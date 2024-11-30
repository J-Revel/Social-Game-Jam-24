using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class TransactionManager : MonoBehaviour
{
    private static TransactionManager singleton;
    [SerializeField] public MoneyBag moneyBag;
    [SerializeField] public AlimentBag alimentBag;
    public static void RegisterTransaction(TransactionEventData data)
    {
        singleton.AddTransactionToHistoric(data);
    }

    private static List<TransactionEventData> transactions = new();
    private void Awake() {singleton = this;}
    private void AddTransactionToHistoric(TransactionEventData data)
    {
        transactions.Add(data);
        moneyBag.PayForTransaction(data);
        alimentBag.AddFromTransaction(data);
    }
}
