using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private Transform popup;
    [SerializeField] private Image background;

    public bool IsDisplayed => popup.gameObject.activeSelf;

    public void Hide()
    {
        background.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
        openButton.gameObject.SetActive(true);
    }

    public void Show()
    {
        background.gameObject.SetActive(true);
        popup.gameObject.SetActive(true);
        openButton.gameObject.SetActive(false);
    }
}