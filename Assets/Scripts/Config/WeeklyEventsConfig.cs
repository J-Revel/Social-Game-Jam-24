using UnityEngine;

[CreateAssetMenu(fileName = "WeeklyEventsConfig", menuName = "Scriptable Objects/WeeklyEventsConfig")]
public class WeeklyEventsConfig : ScriptableObject
{
    public WeeklyEventConfig[] AvaillableEvents;
}