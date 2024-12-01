using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardBonusPopup : MonoBehaviour
{
    public PlayingCardConfig card_received;
    public DeckPanel deck_panel;

    public Transform card_display_container;
    public float add_card_delay = 2;
    public float destroy_delay = 5;

    IEnumerator Start()
    {
        var card = Instantiate(card_received.card_prefab, card_display_container);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(add_card_delay);
        deck_panel.AddCard(card_received);
        yield return new WaitForSeconds(destroy_delay);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
