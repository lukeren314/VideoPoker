using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardLogic
{
    public enum HandType
    {
        ROYAL_FLUSH,
        STRAIGHT_FLUSH,
        FOUR_KIND,
        FULL_HOUSE,
        FLUSH,
        STRAIGHT,
        THREE_KIND,
        TWO_PAIR,
        JACKS_BETTER,
        OTHER
    }
    public static Dictionary<int, int> valueCounts;
    public static Dictionary<Card.Suit, int> suitCounts;
    public static HandType GetHandType(Card[] hand)
    {
        SetCounts(hand);
        
        if(IsFlush(suitCounts)) 
        {
            if (IsRoyal(valueCounts))
            {
                return HandType.ROYAL_FLUSH;
            }
            if (IsStraight(hand)) 
            {
                return HandType.STRAIGHT_FLUSH;
            }
            return HandType.FLUSH;
        }
        if (IsFourKind(valueCounts))
        {
            return HandType.FOUR_KIND;
        }
        if (IsFullHouse(valueCounts))
        {
            return HandType.FULL_HOUSE;
        }
        if (IsStraight(hand)) 
        {
            return HandType.STRAIGHT;
        }
        if (IsThreeKind(valueCounts))
        {
            return HandType.THREE_KIND;
        }
        if (IsTwoPair(valueCounts))
        {
            return HandType.TWO_PAIR;
        }
        if (IsJacksBetter(valueCounts))
        {
            return HandType.JACKS_BETTER;
        }
        return HandType.OTHER;
    }
    public static int GetReturnRatio(HandType handType)
    {
        switch (handType)
        {
            case HandType.ROYAL_FLUSH:
                return 800;
            case HandType.STRAIGHT_FLUSH:
                return 50;
            case HandType.FOUR_KIND:
                return 25;
            case HandType.FULL_HOUSE:
                return 9;
            case HandType.FLUSH:
                return 6;
            case HandType.STRAIGHT:
                return 4;
            case HandType.THREE_KIND:
                return 3;
            case HandType.TWO_PAIR:
                return 2;
            case HandType.JACKS_BETTER:
                return 1;
            default:
                return 0;
        }
    }
    private static void SetCounts(Card[] hand)
    {
        valueCounts = new Dictionary<int, int>();
        suitCounts = new Dictionary<Card.Suit, int>();
        foreach (Card card in hand)
        {
            if (valueCounts.ContainsKey(card.value))
            {
                valueCounts[card.value]++;
            }
            else
            {
                valueCounts.Add(card.value, 1);
            }
            if (suitCounts.ContainsKey(card.suit))
            {
                suitCounts[card.suit]++;
            }
            else
            {
                suitCounts.Add(card.suit, 1);
            }
        }
    }
    private static bool IsFlush(Dictionary<Card.Suit, int> suitCounts)
    {
        return suitCounts.Count == 1;
    }
    private static bool IsStraight(Card[] hand)
    {
        int[] values = new int[hand.Length];
        for(int i = 0; i < hand.Length; i++)
        {
            values[i] = hand[i].value;
        }
        Array.Sort(values);
        for (int i = 0; i < values.Length - 1; i++)
        {
            if(values[i] % 13 + 1 != values[i + 1])
            {
                return false;
            }
        }
        return true;
    }
    private static bool IsRoyal(Dictionary<int, int> valueCounts)
    {
        return valueCounts.ContainsKey(1) &&
            valueCounts.ContainsKey(10) &&
            valueCounts.ContainsKey(11) &&
            valueCounts.ContainsKey(12) &&
            valueCounts.ContainsKey(13);
    }
    private static bool IsFourKind(Dictionary<int, int> valueCounts)
    {
        return valueCounts.ContainsValue(4);
    }
    private static bool IsFullHouse(Dictionary<int, int> valueCounts)
    {
        return valueCounts.ContainsValue(3) && valueCounts.ContainsValue(2);
    }
    private static bool IsThreeKind(Dictionary<int, int> valueCounts)
    {
        return valueCounts.ContainsValue(3);
    }
    private static bool IsTwoPair(Dictionary<int, int> valueCounts)
    {
        int pairCount = 0;
        foreach (int count in valueCounts.Values)
        {
            if (count == 2)
            {
                pairCount++;
            }
        }
        return pairCount == 2;
    }
    private static bool IsJacksBetter(Dictionary<int, int> valueCounts)
    {
        foreach(KeyValuePair<int, int> pair in valueCounts)
        {
            if(pair.Value == 2 && (pair.Key >= 11 || pair.Key == 1))
            {
                return true;
            }
        }
        return false;
    }
}
