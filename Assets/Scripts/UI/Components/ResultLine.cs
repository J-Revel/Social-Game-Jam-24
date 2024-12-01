using TMPro;
using UnityEngine;

public class ResultLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI countText;
    public void SetResult(int score)
    {
        countText.text = score.ToString();
    }
}
