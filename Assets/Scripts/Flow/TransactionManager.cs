using System.Collections.Generic;
using UnityEngine;

public class TransactionManager : MonoBehaviour
{
    public static TransactionManager singleton { get; private set;}
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
        moneyBag?.PayForTransaction(data);
        alimentBag?.AddFromTransaction(data);
    }
}
