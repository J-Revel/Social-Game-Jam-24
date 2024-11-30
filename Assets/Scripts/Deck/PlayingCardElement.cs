using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayingCardElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public PlayingCardConfig config;
    public System.Action hover_start_delegate;
    public System.Action hover_end_delegate;
    public System.Action clicked_delegate;
    public Button button;
    public Image background_image;
    public Image outline_image;
    public Color default_background_color = Color.white;
    public Color selected_background_color = Color.red;
    public Color disabled_background_color = Color.gray;

    public RectTransform rect_transform { get; private set; }

    private void Start()
    {
        rect_transform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        background_image.sprite = config.image;
        outline_image.enabled = false; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover_start_delegate?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover_end_delegate?.Invoke();
    }

    public void SetStateSelected()
    {
        background_image.color = selected_background_color;
        outline_image.enabled = true; 
    }

    public void SetStateDefault()
    {
        background_image.color = default_background_color;
        outline_image.enabled = false; 
    }

    public void SetStateDisabled()
    {
        background_image.color = disabled_background_color;
        outline_image.enabled = false; 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked_delegate?.Invoke();
    }
}
