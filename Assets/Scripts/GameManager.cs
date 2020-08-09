using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        FINISHED,
        BETTING,
        MULLIGAN
    }
    public GameState gameState;
    public Player player;

    public GameEvents gameEvents;
    public UIManager uIManager;
    public CardManager cardManager;
    public SlotManager slotManager;

    public void NextState()
    {
        switch (gameState)
        {
            case GameState.FINISHED:
                StartRound();
                break;
            case GameState.BETTING:
                if (uIManager.ValidBettingValue())
                {
                    StartMulligan();
                }
                break;
            case GameState.MULLIGAN:
                EndRound();
                break;
        }
    }
    public void OnStart()
    {
        uIManager.SetCreditsText(player.credits);
    }
    public void OnRoundBegin()
    {
        player.bet = 0;
        uIManager.SetCreditsBetText(player.credits);
    }
    public void OnMulliganBegin()
    {
        player.bet = uIManager.GetBettingAmount();
        player.credits -= player.bet;
        
        uIManager.SetCreditsBetText(player.bet);
        uIManager.SetCreditsText(player.credits);

        DealHand();
    }
    public void DealHand()
    {
        Card[] hand = cardManager.DrawCards(slotManager.slots.Count);
        slotManager.SetHand(hand);
    }
    public void OnRoundEnd()
    {
        List<Card> replacedCards = slotManager.GetReplacedCards();

        Card[] newCards = cardManager.DrawCards(replacedCards.Count);
        slotManager.ReplaceCards(newCards);

        Card[] hand = slotManager.GetHand();
        CardLogic.HandType handType = CardLogic.GetHandType(hand);
        int returnRatio = CardLogic.GetReturnRatio(handType);
        int totalReturn = returnRatio * player.bet;

        player.credits += totalReturn;
        uIManager.SetRoundLogText(totalReturn - player.bet);

        //calculate return, log it to ui
    }
    private void Start()
    {
        if (player == null)
        {
            player = new Player();
        }
        gameEvents.OnStart.Invoke();
    }
    private void StartRound()
    {
        gameEvents.OnRoundBegin.Invoke();
        gameState = GameState.BETTING;
    }
    private void StartMulligan()
    {
        gameEvents.OnMulliganBegin.Invoke();
        gameState = GameState.MULLIGAN;
    }
    private void EndRound()
    {
        gameEvents.OnRoundEnd.Invoke();

        gameState = GameState.FINISHED;
    }
}
