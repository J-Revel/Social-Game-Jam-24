using UnityEngine;
using TMPro;

public class StartPanel : MonoBehaviour
{
    [Header("EventArea")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    public void Set()
    {
        var currentEvent = Global.CurrentWeeklyEvent;
        titleText.text = currentEvent.TitleLocKey.GetLocalizedString();
        descriptionText.text = currentEvent.DescriptionLocKey.GetLocalizedString();
    }
}