using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "DeckElement", menuName = "Deck/Card")]
public class PlayingCardConfig : ScriptableObject, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite image;
    public CardTag[] tags;
    public string text;
    private RectTransform rect_transform;

    public float default_size;
    public float hovered_size;
    public float hover_anim_duration;

    private bool hovered;
    private float hovered_anim_time;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }


    public void Update()
    {
        if (hovered)
            hovered_anim_time += Time.deltaTime;
        else hovered_anim_time -= Time.deltaTime;
        hovered_anim_time = Mathf.Clamp(hovered_anim_time, 0, hover_anim_duration);
        rect_transform.sizeDelta = new Vector2(Mathf.Lerp(default_size, hovered_size, hovered_anim_time / hover_anim_duration), 0);
    }
}
