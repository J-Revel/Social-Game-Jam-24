using UnityEngine;

public class Gauge : MonoBehaviour
{
    [SerializeField] public RectTransform fillingRectTransform;
    public float Value = 0f;
    public void Update()
    {
        fillingRectTransform.anchorMax = new Vector2(Value, fillingRectTransform.anchorMax.y);
    }
}
