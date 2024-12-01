using System;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    public static HelpManager singleton;
    [SerializeField] private Button confirmationButton;
    [SerializeField] private Image explanationOverlayImage;
    [SerializeField] private HelpData[] shopHelpDataList;
    [SerializeField] private HelpData mapHelpData;

    public bool IsDisplayingHelp => explanationOverlayImage.IsActive();
    private int currentWatchedIndex = -1;

    void Awake()
    {
        singleton = this;
    }

    public void HideHelp()
    {
        explanationOverlayImage.enabled = false;
        confirmationButton.gameObject.SetActive(false);
    }

    private void DisplayHelp(HelpData data)
    {
        data.HasBeenDisplayed = true;
        explanationOverlayImage.sprite = data.ExplaningSprite;
        //TODO reactivate help
        //confirmationButton.gameObject.SetActive(true);
        //explanationOverlayImage.enabled = true;
    }

    public static void DisplayHelp() => singleton.DisplayHelp(singleton.GetCurrentHelpData());

    public static void OnShopOpen(int shopIndex)
    {
        singleton.currentWatchedIndex = shopIndex;
        singleton.TryDisplayHelp();
    }

    public static void OnMapOpen()
    {
        singleton.currentWatchedIndex = -1;
        singleton.TryDisplayHelp();
    }

    public static void Reset()
    {
        foreach(HelpData data in singleton.shopHelpDataList) data.HasBeenDisplayed = false;
        singleton.mapHelpData.HasBeenDisplayed = false;
    }

    private void TryDisplayHelp()
    {
        HelpData dataToActivate = GetCurrentHelpData();
        if (!dataToActivate.HasBeenDisplayed)
        {
            DisplayHelp(dataToActivate);
        }
    }

    private HelpData GetCurrentHelpData()
    {
        return currentWatchedIndex switch
        {
            -1 => mapHelpData,
            _ => shopHelpDataList[currentWatchedIndex],
        };
    }

    
}

[Serializable]
public class HelpData
{
    public bool HasBeenDisplayed = false;
    public Sprite ExplaningSprite;
}
