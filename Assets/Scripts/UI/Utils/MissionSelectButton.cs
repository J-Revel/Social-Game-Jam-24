using System.Linq;
using UnityEngine;

public class MissionSelectButton : MonoBehaviour
{
    public WeeklyEventConfig mission;
    public TMPro.TMP_Text text;

    public void Start()
    {
        if(text != null)
            text.text = mission.TitleText;
    }
    public void SelectMission()
    {
        Global.CurrentWeeklyEvent = mission;
    }

    public void SelectRandomMission()
    {
        var potentialEvents = Global.WeeklyEventsConfig.AvaillableEvents.Where(e => e != Global.CurrentWeeklyEvent).ToArray();
        Global.CurrentWeeklyEvent = potentialEvents[UnityEngine.Random.Range(0, potentialEvents.Length)];
    }
}
