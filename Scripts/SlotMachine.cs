using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
    [Header("Game Settings")]
    public int balance = 1000;
    public int bet = 10;
    
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
    private SymbolData[] currentSymbols = new SymbolData[9];

    public HelpPageManager helpPageManager;
    
    void Start()
    {
        UpdateUI();
    }
    
    public void OnSpinClicked()
    {
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
        
        // Animate spinning (2 seconds)
        float spinTime = 0;
        while (spinTime < 0.05f)
        {
            spinTime += Time.deltaTime;
            
            // Show random symbols during spin
            for (int i = 0; i < 9; i++)
            {
                SymbolData randomSymbol = allSymbols[Random.Range(0, allSymbols.Count)];
                reelImages[i].sprite = randomSymbol.symbolSprite;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        
        // Final result
        for (int i = 0; i < 9; i++)
        {
            currentSymbols[i] = allSymbols[1];//allSymbols[Random.Range(0, allSymbols.Count)];
            reelImages[i].sprite = currentSymbols[i].symbolSprite;
        }
        
        // Check for wins
        int winAmount = CheckWins();
        
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
  
    }
    
    int CheckWins()
    {
        int totalWin = 0;
        
        // Check 5 paylines
        totalWin += CheckLine(0, 1, 2);   // Top row
        totalWin += CheckLine(3, 4, 5);   // Middle row
        totalWin += CheckLine(6, 7, 8);   // Bottom row
        totalWin += CheckLine(0, 4, 8);   // Diagonal \
        totalWin += CheckLine(2, 4, 6);   // Diagonal /
        
        return totalWin * bet;
    }
    
    int CheckLine(int pos1, int pos2, int pos3)
    {
        SymbolData sym1 = currentSymbols[pos1];
        SymbolData sym2 = currentSymbols[pos2];
        SymbolData sym3 = currentSymbols[pos3];
        
        // Replace wilds
        if (sym1.isWild) sym1 = sym2.isWild ? sym3 : sym2;
        if (sym2.isWild) sym2 = sym1;
        if (sym3.isWild) sym3 = sym1;
        
        // Check if all match
        if (sym1.symbolID == sym2.symbolID && sym2.symbolID == sym3.symbolID)
        {
            return sym1.payoutValue;
        }
        
        return 0;
    }
    
    public void OnIncreaseBet()
    {
        if (!isSpinning)
        {
            bet += 5;
            if (bet > 25) bet = 25;
            UpdateUI();
        }
    }
    
    public void OnDecreaseBet()
    {
        if (!isSpinning)
        {
            bet -= 5;
            if (bet < 5) bet = 5;
            UpdateUI();
        }
    }
    
    void UpdateUI()
    {
        balanceText.text = $"${balance}";
        betText.text = $"${bet}";
        spinButton.interactable = !isSpinning && balance >= bet;
    }
}