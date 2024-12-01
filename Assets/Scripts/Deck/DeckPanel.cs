using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPanel : MonoBehaviour, IPointerExitHandler
{
    public PlayingCardConfig[] deck;
    public PlayingCardElement playing_card_prefab;
    private List<PlayingCardElement> playing_cards = new List<PlayingCardElement>();
    public float default_spacing = 40;
    public float hover_spacing = 130;
    public float hovered_card_anim_cursor = -1;
    public int hovered_card = -1;
    public float hovered_anim_duration = 0.3f;
    public float hover_cursor_anim_speed = 3;
    public float fold_effect_duration = 0.3f;
    private List<HoverEffect> hover_effects = new List<HoverEffect>();
    public float fold_effect_time = 0;
    private List<PlayingCardElement> selected_cards = new List<PlayingCardElement>();
    public bool allow_selection;
    public ProductTag[] selected_product_tags;
    public Vector2 hover_additional_offset;
    public float appear_speed = 3;
    public Vector2 appear_offset = new Vector2(100, -100);


    public class HoverEffect
    {
        public float value;
        public float target_value;
        public float appear_value;
    }

    void Start()
    {
        int cursor = 0;
        foreach (PlayingCardConfig deck_element in deck)
        {
            AddCard(deck_element);
        }
    }

    void Update()
    {
        float target_direction = Mathf.Sign(hovered_card - hovered_card_anim_cursor);
        if (Mathf.Abs(hovered_card - hovered_card_anim_cursor) > hover_cursor_anim_speed * Time.deltaTime)
            hovered_card_anim_cursor += target_direction * hover_cursor_anim_speed * Time.deltaTime;
        else
            hovered_card_anim_cursor = hovered_card;
        float weight_sum = 0;
        bool folded = true;
        for (int i = 0; i < hover_effects.Count; i++)
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
            hover_effect.appear_value = Mathf.Clamp01(hover_effect.appear_value + appear_speed * Time.deltaTime);
            hover_effects[i] = hover_effect;

            weight_sum += hover_effect.value;
        }
        if (folded)
            fold_effect_time -= Time.deltaTime;
        else fold_effect_time += Time.deltaTime;

        
        fold_effect_time = Mathf.Clamp(fold_effect_time, 0, fold_effect_duration);
        float fold_effect_ratio = fold_effect_time / fold_effect_duration;
        float cursor = 0;
        for (int i = 0; i < playing_cards.Count; i++)
        {
            float value = 0;
            if (weight_sum > 0)
            {
                value = hover_effects[i].value / weight_sum * fold_effect_ratio;
            }
            cursor += Mathf.Lerp(default_spacing, hover_spacing, value);
        }
        float used_width = cursor;
        cursor = 0;
        for (int i = 0; i < playing_cards.Count; i++)
        {
            float value = 0;
            if (weight_sum > 0)
            {
                value = hover_effects[i].value / weight_sum * fold_effect_ratio;
            }
            playing_cards[i].rect_transform.anchoredPosition = appear_offset * (1-hover_effects[i].appear_value) + new Vector2(cursor - used_width / 2, 0) + hover_effects[i].value * hover_additional_offset;
            cursor += Mathf.Lerp(default_spacing, hover_spacing, value);
        }
    }

    public void AddCard(PlayingCardConfig card)
    {
        PlayingCardElement playing_card = Instantiate(playing_card_prefab, transform);
        playing_card.config = card;
        playing_cards.Add(playing_card);
        HoverEffect effect = new HoverEffect { };
        playing_card.hover_effect = effect;
        hover_effects.Add(effect);
        playing_card.hover_start_delegate += () =>
        {
            if (hovered_card >= 0)
                hover_effects[hovered_card].target_value = 0;
            for (int i = 0; i < playing_cards.Count; i++)
                if (playing_cards[i] == playing_card)
                    hovered_card = i;
            effect.target_value = 1;
        };
        playing_card.hover_end_delegate += () =>
        {
            //hover_effects[card_index].target_value = 0;
        };
        playing_card.clicked_delegate += () =>
        {
            if (allow_selection)
            {
                bool tags_compatible = true;
                foreach (var tag_filter in playing_card.config.tag_filters)
                {
                    if (!tag_filter.IsCompatibleWithTags(selected_product_tags))
                    {
                        tags_compatible = false;
                    }
                }
                if (tags_compatible)
                {
                    if (selected_cards.Contains(playing_card))
                    {
                        selected_cards.Remove(playing_card);
                        playing_card.SetStateDefault();
                    }
                    else
                    {
                        selected_cards.Add(playing_card);
                        playing_card.SetStateSelected();
                    }
                }
            }
        };
    }

    public void StartCardSelection(ProductTag[] product_tags)
    {
        allow_selection = true;
        selected_product_tags = product_tags;
        foreach(var card in playing_cards)
        {
            bool tags_compatible = true;
            foreach (var tag_filter in card.config.tag_filters)
            {
                if (!tag_filter.IsCompatibleWithTags(selected_product_tags))
                {
                    tags_compatible = false;
                }
            }
            if (tags_compatible)
                card.SetStateDefault();
            else card.SetStateDisabled();
            
        }
    }
    public void StopCardSelection()
    {
        allow_selection = false;
        foreach(var card in playing_cards)
        {
            card.SetStateDefault();
        }
    }

    public void ConsumeSelectedCards()
    {
        for(int i=selected_cards.Count-1; i>=0; i--)
        {
            Destroy(selected_cards[i].gameObject);
            playing_cards.Remove(selected_cards[i]);
            hover_effects.Remove(selected_cards[i].hover_effect);
        }
        selected_cards.Clear();
    }

    public float selected_price_multiplier { get
        {
            float result = 1;
            foreach(PlayingCardElement card in selected_cards)
            {
                result *= card.config.price_multiplier;
            }
            return result;
        } 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(hovered_card >= 0)
        {
            hover_effects[hovered_card].target_value = 0;
            hovered_card = 0;
        }
    }
}
