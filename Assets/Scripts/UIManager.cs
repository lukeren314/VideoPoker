using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text headText;
    public Text creditsText;
    public InputField bettingInputField;
    public Text creditsBetText;
    public Text roundLogText;
    public void OnStart()
    {
        headText.text = "Press Next to Begin!";
        SetBettingActive(false);
        SetRoundLogActive(false);
    }
    public void OnRoundBegin()
    {
        headText.text = "Enter # of credits to bet.";
        SetCreditsBetText(0);
        SetBettingActive(true);
        SetRoundLogActive(false);
    }
    public void OnMulliganBegin()
    {
        SetBettingActive(false);
        headText.text = "Click on the cards you want to replace.";
    }
    public void OnRoundEnd()
    {
        SetRoundLogActive(true);
        headText.text = "Click Next to start the next round.";
    }
    public void SetCreditsText(int credits)
    {
        creditsText.text = "Credits: " + credits;
    }
    public void SetCreditsBetText(int creditsBet)
    {
        creditsBetText.text = "Credits Bet: " + creditsBet;
    }
    public void SetBettingActive(bool isActive)
    {
        bettingInputField.gameObject.SetActive(isActive);
    }
    public void SetRoundLogActive(bool isActive)
    {
        roundLogText.gameObject.SetActive(isActive);
    }
    public string GetBettingText()
    {
        return bettingInputField.text;
    }
    public void SetRoundLogText(int profit)
    {
        string newText;
        if(profit == 0)
        {
            newText = "You broke even. You won 0 credits.";
        } else if (profit > 0)
        {
            newText = "You won! You won " + profit + " credits.";
        }
        else
        {
            newText = "You lost. You lost " + profit + " credits.";
        }
        roundLogText.text = newText;
    }
    
}
