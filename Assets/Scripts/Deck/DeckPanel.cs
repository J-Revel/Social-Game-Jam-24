using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckPanel: MonoBehaviour
{
    public PlayingCardConfig[] deck;
    public PlayingCardElement playing_card_prefab;
    private PlayingCardElement[] playing_cards;
    public float default_spacing = 40;
    public float hover_spacing = 130;
    public float hovered_card_anim_cursor = -1;
    public int hovered_card = -1;
    private float hovered_anim_time = 0;
    public float hovered_anim_duration = 0.3f;
    public float hover_cursor_anim_speed = 3;
    public float fold_effect_duration = 0.3f;
    private HoverEffect[] hover_effects;
    public float fold_effect_time = 0;
    private List<int> selected_cards = new List<int>();


    private struct HoverEffect
    {
        public int index;
        public float value;
        public float target_value;
    }

    void Start()
    {
        playing_cards = new PlayingCardElement[deck.Length];
        hover_effects = new HoverEffect[deck.Length];
        int cursor = 0;
        foreach(PlayingCardConfig deck_element in deck)
        {
            PlayingCardElement playing_card = Instantiate(playing_card_prefab, transform);
            playing_card.config = deck_element;
            playing_cards[cursor] = playing_card;
            int card_index = cursor;
            playing_card.hover_start_delegate += () =>
            {
                hover_effects[card_index].target_value = 1;
            };
            playing_card.hover_end_delegate += () =>
            {
                hover_effects[card_index].target_value = 0;
            };
            playing_card.clicked_delegate += () =>
            {
                if(selected_cards.Contains(card_index))
                {
                    selected_cards.Remove(card_index);
                    playing_cards[card_index].SetSelected(false);
                }
                else
                {
                    selected_cards.Add(card_index);
                    playing_cards[card_index].SetSelected(true);
                }
            };
            cursor++;
        }
    }

    void Update()
    {
        float target_direction = Mathf.Sign(hovered_card - hovered_card_anim_cursor);
        if (Mathf.Abs(hovered_card - hovered_card_anim_cursor) > hover_cursor_anim_speed * Time.deltaTime)
            hovered_card_anim_cursor += target_direction * hover_cursor_anim_speed * Time.deltaTime;
        else
            hovered_card_anim_cursor = hovered_card;
        float cursor = 0;
        float weight_sum = 0;
        bool folded = true;
        for(int i=0; i<hover_effects.Length; i++)
        {
            var hover_effect = hover_effects[i];
            float offset = hover_effect.target_value - hover_effect.value;
            if (hover_effect.target_value > 0)
                folded = false;
            if (Mathf.Abs(offset) < hover_cursor_anim_speed * Time.deltaTime)
            {
                hover_effect.value = hover_effect.target_value;
            }
            else hover_effect.value += Mathf.Sign(offset) * Time.deltaTime * hover_cursor_anim_speed;
            hover_effects[i] = hover_effect;

            weight_sum += hover_effect.value;
        }
        if (folded)
            fold_effect_time -= Time.deltaTime;
        else fold_effect_time += Time.deltaTime;
        fold_effect_time = Mathf.Clamp(fold_effect_time, 0, fold_effect_duration);
        float fold_effect_ratio = fold_effect_time / fold_effect_duration;
        for(int i=0; i<playing_cards.Length; i++)
        {
            float value = 0;
            if(weight_sum > 0)
            {
                value = hover_effects[i].value / weight_sum * fold_effect_ratio;
            }
            playing_cards[i].rect_transform.anchoredPosition = new Vector2(cursor - playing_cards.Length * default_spacing / 2, 0);
            cursor += Mathf.Lerp(default_spacing, hover_spacing, value);
        }
    }
}
