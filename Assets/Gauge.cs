using UnityEngine;

public class Gauge : MonoBehaviour
{
    [SerializeField] public RectTransform fillingRectTransform;
    public float Value;
    public void Update()
    {
        fillingRectTransform.anchorMax = new Vector2(Value, fillingRectTransform.anchorMax.y);
    }
}
