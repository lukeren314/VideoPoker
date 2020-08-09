using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards;
    private int drawIndex;
    public void OnRoundBegin()
    {
        drawIndex = 0;
        ShuffleCards();
    }
    public void ShuffleCards()
    {
        for(int i = 0; i < cards.Count-1; i++)
        {
            int j = Random.Range(i, cards.Count-1);
            Card temp;
            temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }
    public Card[] DrawCards(int handSize)
    {
        Card[] hand = new Card[handSize];
        for (int i = 0; i < handSize; i++)
        {
            hand[i] = cards[drawIndex];
            drawIndex++;
        }
        return hand;
    }
}
