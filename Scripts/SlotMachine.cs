using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SlotMachine : MonoBehaviour
{
    public FreeGameManager fgm;
    [Header("Game Settings")]
    public float balance = 1000f;
    public float bet = 10f;
    
    [Header("All Symbols")]
    public List<SymbolData> allSymbols;  // 12 SymbolData here
    
    [Header("Reel Images (3x3 = 9 images)")]
    public Image[] reelImages;  // Size 15: [0-4]=Reel1, [5-9]=Reel2, [10-14]=Reel3
    
    [Header("UI Text")]
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI messageText;
    
    [Header("Buttons")]
    public Button spinButton;
    public Button increaseBetButton;
    public Button decreaseBetButton;
    
    private bool isSpinning = false;
    private SymbolData[] currentSymbols = new SymbolData[15];

    public HelpPageManager helpPageManager;
    public PaylineData payLineData = new PaylineData();

    public string currentSymbol = "";

    public float baseGameReelSpinCount = 0;

    public GameObject[] matchingLines;

    public SoundManager soundManager;

    public bool gameWon = false;

    public int randomIndex = 0;

    void Start()
    {
        soundManager.loadingAudio.Play();
        UpdateUI();
    }
    
    public void OnSpinClicked()
    {
        soundManager.buttonClickAudio.Play();
        foreach(GameObject line in matchingLines)
        {
            line.SetActive(false);
        }

        if (isSpinning || balance < bet) return;
        StartCoroutine(Spin());
    }
    
    IEnumerator Spin()
    {
        helpPageManager.infoButton.interactable = false;
        isSpinning = true;
        balance -= bet;
        messageText.text = "GOODLUCK";
        UpdateUI();

        soundManager.baseGameAudio.volume = 0.2f;
        soundManager.reelSpinAudio.Play();
        // Animate spinning (2 seconds)
        float spinTime = 0;
        while (spinTime < 0.2f)
        {
            spinTime += Time.deltaTime;

            // Show random symbols during spin
            for (int i = 0; i < 15; i++)
            {
                SymbolData randomSymbol = allSymbols[UnityEngine.Random.Range(0, allSymbols.Count)];
                reelImages[i].sprite = randomSymbol.symbolSprite;
            }
            
            yield return new WaitForSeconds(0.1f);
        }

        // Final result
        float winAmount = 0;

        if (baseGameReelSpinCount % 2 == 0)
        {
            gameWon = true;
            winAmount = PlayFinalStops() * bet;
        }

        else
        {
            gameWon = false;
            winAmount = 0;
        }
        
        if (winAmount > 0)
        {
            balance += winAmount;
            messageText.text = $"WIN ${winAmount}!";
            winText.text = $"${winAmount}";
        }
        else
        {
            messageText.text = "Try Again!";
            winText.text = "$0";
        }

        isSpinning = false;
        helpPageManager.infoButton.interactable = true;
        UpdateUI();
        baseGameReelSpinCount++;

        if (randomIndex == 14 || randomIndex == 15)
            soundManager.bonusLandStopAudio.Play();
        else if (randomIndex == 12 || randomIndex == 13)
            soundManager.wildStopAudio.Play();
        else if (gameWon)
            soundManager.baseGameStopAudio.Play();
        else
            soundManager.gameLostAudio.Play();

        soundManager.reelSpinAudio.Pause();
        soundManager.reelStopAudio.Play();
        soundManager.baseGameAudio.volume = 1f;
    }

    public float PlayFinalStops()
    {
        randomIndex = UnityEngine.Random.Range(0, payLineData.lineWins.Count);

        var SymbolWinpair = payLineData.lineWins.ElementAt(randomIndex);

        if(randomIndex == 14 || randomIndex == 15)
        {
            fgm.OnDoubleClickSpinBtn();
        }

        else
        {
            List<string> payLineString = SymbolWinpair.Key;

            for (int i = 0; i < 15; i++)
            {
                currentSymbol = payLineString[i];
                SymbolData bonusSymbol = allSymbols.Find(s => s.name == currentSymbol);
                currentSymbols[i] = bonusSymbol;
                reelImages[i].sprite = currentSymbols[i].symbolSprite;
            }
            matchingLines[randomIndex].SetActive(true);
            return SymbolWinpair.Value;
        }
        return 0;
    }
    
    public void OnIncreaseBet()
    {
        soundManager.buttonClickAudio.Play();

        if (!isSpinning)
        {
            bet += 10;
            if (bet > 25) bet = 25;
            UpdateUI();
        }
    }
    
    public void OnDecreaseBet()
    {
        soundManager.buttonClickAudio.Play();

        if (!isSpinning)
        {
            bet -= 5;
            if (bet < 10) bet = 10;
            UpdateUI();
        }
    }
    
    public void UpdateUI()
    {
        balanceText.text = $"${balance}";
        betText.text = $"${bet}";
        spinButton.interactable = !isSpinning && balance >= bet;
    }
}