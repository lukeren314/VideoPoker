using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Card card;
    public Text holdText;

    private bool _hold;
    public bool hold { 
        get { return _hold; } 
        set {
            _hold = value;
            holdText.text = _hold ? "Hold" : "Replace";
        } 
    }
    public void AssignCard(Card card)
    {
        this.card = card;
        GetComponent<Image>().sprite = card.sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        hold = !hold;
    }
}
