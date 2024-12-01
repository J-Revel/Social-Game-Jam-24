using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardBonusPopup : MonoBehaviour
{
    public PlayingCardConfig card_received;
    public DeckPanel deck_panel;

    public Image image;
    public float add_card_delay = 2;
    public float destroy_delay = 5;

    IEnumerator Start()
    {
        image.sprite = card_received.image;
        yield return new WaitForSeconds(add_card_delay);
        deck_panel.AddCard(card_received);
        yield return new WaitForSeconds(destroy_delay);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
