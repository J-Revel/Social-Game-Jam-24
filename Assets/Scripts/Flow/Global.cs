using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Global : MonoBehaviour
{
    private static Global singleton;

    [Header("Configs")]
    [SerializeField] private WeeklyEventsConfig weeklyEventsConfig;
    [SerializeField] private ShopConfig[] shopConfigs;

    public static WeeklyEventsConfig WeeklyEventsConfig => singleton.weeklyEventsConfig;
    public static ShopConfig[] ShopConfigs => singleton.shopConfigs;

    private WeeklyEventConfig currentWeeklyEvent;
    private ShopContentData[] currentShopList;

    public static WeeklyEventConfig CurrentWeeklyEvent {get => singleton.currentWeeklyEvent; set => singleton.currentWeeklyEvent = value;}
    public static ShopContentData[] CurrentShopList {get => singleton.currentShopList; set => singleton.currentShopList = value;}

    [Header("Navigation")]
    [SerializeField] private NavigationController navigationController;

    public static void GoToState(GameState nextGameState)
    {
        singleton.navigationController.GoToState(nextGameState);
    }
            
    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        navigationController.Init();
    }
}