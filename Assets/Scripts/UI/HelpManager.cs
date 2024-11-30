using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private Button confirmationButton;
    [SerializeField] private Image explanationOverlayImage;
    [SerializeField] private List<HelpData> helpDataList;

    public bool IsDisplayingHelp => explanationOverlayImage.IsActive();

    private void Update()
    {
        if(IsDisplayingHelp)
        {
            return;
        }

        HelpData dataToActivate = SearchData(data => !data.HasBeenDisplayed && data.TriggerObject.activeSelf);
        if(dataToActivate!=null)
        {
            DisplayHelp(dataToActivate);
        }
    }

    public void DisplayHelp() => DisplayHelp(SearchData(data => data.TriggerObject.activeSelf));

    public void HideHelp()
    {
        explanationOverlayImage.enabled = false;
        openButton.gameObject.SetActive(true);
        confirmationButton.gameObject.SetActive(false);
    }

    private void DisplayHelp(HelpData data)
    {
        data.HasBeenDisplayed = true;
        explanationOverlayImage.enabled = true;
        explanationOverlayImage.sprite = data.ExplaningSprite;
        openButton.gameObject.SetActive(false);
        confirmationButton.gameObject.SetActive(true);
    }

    private HelpData? SearchData(Func<HelpData, bool> filter)
    {
        foreach(HelpData data in helpDataList)
        {
            if(filter(data)){return data;}
        }
        return null;
    }
}

[Serializable]
public class HelpData
{
    public bool HasBeenDisplayed = false;
    public GameObject TriggerObject;
    public Sprite ExplaningSprite;
}
