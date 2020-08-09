using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public List<CardSlot> slots;
    public void OnStart()
    {
        SetSlotsActive(false);
    }
    public void OnRoundBegin()
    {
        SetSlotsActive(false);
    }
    public void OnMulliganBegin()
    {
        SetSlotsActive(true);
        SetSlotsHold(true);
    }
    public void SetHand(Card[] hand)
    {
        for(int i = 0; i < hand.Length; i++)
        {
            slots[i].AssignCard(hand[i]);
        }
    }
    public List<Card> GetReplacedCards()
    {
        List<Card> replacedCards = new List<Card>();
        foreach(CardSlot cardSlot in slots)
        {
            if (!cardSlot.hold)
            {
                replacedCards.Add(cardSlot.card);
            }
        }
        return replacedCards;
    }
    public void ReplaceCards(Card[] newCards)
    {
        int i = 0;
        foreach(CardSlot cardSlot in slots)
        {
            if (!cardSlot.hold)
            {
                cardSlot.AssignCard(newCards[i]);
                i++;
            }
        }
    }
    public Card[] GetHand()
    {
        Card[] hand = new Card[slots.Count];
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i] = slots[i].card;
        }
        return hand;
    }
    private void SetSlotsActive(bool isActive)
    {
        foreach (CardSlot cardSlot in slots)
        {
            cardSlot.gameObject.SetActive(isActive);
        }
    }
    private void SetSlotsHold(bool hold)
    {
        foreach(CardSlot cardSlot in slots)
        {
            cardSlot.hold = hold;
        }
    }
    
}
