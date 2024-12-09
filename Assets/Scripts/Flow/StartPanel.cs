using UnityEngine;
using TMPro;
using System.Linq;

public class StartPanel : GamePart
{
    [Header("EventArea")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public override void EndPart()
    {
        this.gameObject.SetActive(false);
    }   

    public override void StartPart()
    {
        this.PickMission();
        UpdateVisual();
        this.gameObject.SetActive(true);
    }

    private void PickMission()
    {
        // Pick Next Event
        var potentialEvents = Global.WeeklyEventsConfig.AvaillableEvents.Where(e => e != Global.CurrentWeeklyEvent).ToArray();
        Global.CurrentWeeklyEvent = potentialEvents[UnityEngine.Random.Range(0, potentialEvents.Length)];
    }

    private void UpdateVisual()
    {
        var currentEvent = Global.CurrentWeeklyEvent;
        titleText.text = currentEvent.TitleText;
        descriptionText.text = currentEvent.DescriptionText;
    }

    public void Next()
    {
        Global.GoToState(GameState.Play);
    }  
}