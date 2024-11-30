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
    private List<int> selected_cards = new List<int>();
    public bool allow_selection;
    public ProductTag[] selected_product_tags;
    public Vector2 hover_additional_offset;


    private class HoverEffect
    {
        public float value;
        public float target_value;
    }

    void Start()
    {
        int cursor = 0;
        foreach (PlayingCardConfig deck_element in deck)
        {
            PlayingCardElement playing_card = Instantiate(playing_card_prefab, transform);
            playing_card.config = deck_element;
            playing_cards.Add(playing_card);
            hover_effects.Add(new HoverEffect { });
            int card_index = cursor;
            playing_card.hover_start_delegate += () =>
            {
                if (hovered_card >= 0)
                    hover_effects[hovered_card].target_value = 0;
                hovered_card = card_index;
                hover_effects[card_index].target_value = 1;
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
                        if (selected_cards.Contains(card_index))
                        {
                            selected_cards.Remove(card_index);
                            playing_cards[card_index].SetStateDefault();
                        }
                        else
                        {
                            selected_cards.Add(card_index);
                            playing_cards[card_index].SetStateSelected();
                        }
                    }

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
            playing_cards[i].rect_transform.anchoredPosition = new Vector2(cursor - used_width / 2, 0) + hover_effects[i].value * hover_additional_offset;
            cursor += Mathf.Lerp(default_spacing, hover_spacing, value);
        }
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
        allow_selection = true;
        foreach(var card in playing_cards)
        {
            card.SetStateDefault();
        }
    }

    public void ConsumeSelectedCards()
    {
        selected_cards.Sort();
        for(int i=0; i<selected_cards.Count; i++)
        {
            int selected_index = selected_cards[i];
            Destroy(playing_cards[selected_index].gameObject);
            playing_cards.RemoveAt(selected_index);
            hover_effects.RemoveAt(selected_index);
        }
        selected_cards.Clear();
    }

    public float selected_price_multiplier { get
        {
            float result = 1;
            foreach(int card_index in selected_cards)
            {
                result *= playing_cards[card_index].config.price_multiplier;
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
