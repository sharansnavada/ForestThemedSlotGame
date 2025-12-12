using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeGameManager : MonoBehaviour
{
   public SlotMachine slotMachine;

   public GameObject greyBackGround;
   public GameObject FreeGameTextImage;

   public Sprite BaseGameImage;
   public Sprite freeGameImage;
   public GameObject BaseGameBackGroundGameObject;
   public Sprite baseGameBG;

   public GameObject gameLogo;
   public GameObject freeSpin_Banner;

   public Button[] buttonPanels;

   public GameObject spinsDataGameObject;
   public TextMeshProUGUI spinsPlayed;

   [Header("All Symbols")]
   public List<SymbolData> allSymbols;  // 12 SymbolData here

   [Header("Reel Images (3x3 = 9 images)")]
   public Image[] reelImages;  // Size 15: [0-4]=Reel1, [5-9]=Reel2, [10-14]=Reel3

   private SymbolData[] currentSymbols = new SymbolData[15];

   public string currentSymbol = "";
   public PaylineData payLineData = new PaylineData();

   public GameObject[] matchingLines;

   public float freeGameReelSpinCount = 0;

   void Start()
   {

   }

   void Update()
   {

   }

   public void OnDoubleClickSpinBtn()
   {
       StartCoroutine(FreeGamesFlow());
   }

   private IEnumerator FreeGamesFlow()
   {
       // FIRST show the free game animation
       yield return StartCoroutine(EnableBGandFGText());

       // THEN enable free game UI
       EnableFreeGameUI();

       // THEN start the free spins
       yield return StartCoroutine(PlayFreeGamesRoutine());

       // FINALLY revert UI
       RevertFreeGameUI();
   }

   IEnumerator EnableBGandFGText()
   {
       greyBackGround.SetActive(true);
       FreeGameTextImage.SetActive(true);

       yield return new WaitForSeconds(3f);

       greyBackGround.SetActive(false);
       FreeGameTextImage.SetActive(false);
   }

   public void EnableFreeGameUI()
   {
       baseGameBG = BaseGameBackGroundGameObject.GetComponent<Sprite>();
       baseGameBG = freeGameImage;

       gameLogo.SetActive(false);
       freeSpin_Banner.SetActive(true);

       DisableButtonPanels();

       spinsDataGameObject.SetActive(true);
   }

   public void RevertFreeGameUI()
   {
       baseGameBG = BaseGameBackGroundGameObject.GetComponent<Sprite>();
       baseGameBG = BaseGameImage;

       gameLogo.SetActive(true);
       freeSpin_Banner.SetActive(false);

       EnableButtonPanels();

       spinsDataGameObject.SetActive(false);
   }

   public void DisableButtonPanels()
   {
       foreach (Button btn in buttonPanels)
       {
           btn.interactable = false;
       }
   }

   public void EnableButtonPanels()
   {
       foreach (Button btn in buttonPanels)
       {
           btn.interactable = true;
       }
   }

   private IEnumerator PlayFreeGamesRoutine()
   {
       freeGameReelSpinCount = 0;

       while (freeGameReelSpinCount < 8)
       {
           foreach (GameObject line in matchingLines)
           {
               line.SetActive(false);
           }

           yield return StartCoroutine(Spin());

           freeGameReelSpinCount++;
           spinsPlayed.text = freeGameReelSpinCount.ToString();
       }
   }

   IEnumerator Spin()
   {
       float spinTime = 0;
       while (spinTime < 0.2f)
       {
           spinTime += Time.deltaTime;

           for (int i = 0; i < 15; i++)
           {
               SymbolData randomSymbol = allSymbols[UnityEngine.Random.Range(0, allSymbols.Count)];
               reelImages[i].sprite = randomSymbol.symbolSprite;
           }

           yield return new WaitForSeconds(0.1f);
       }

       float winAmount = 0;

       winAmount += PlayFinalStops() * slotMachine.bet;

       if (winAmount > 0)
       {
           slotMachine.balance += winAmount;
           slotMachine.messageText.text = $"WIN ${winAmount}!";
           slotMachine.winText.text = $"${winAmount}";
       }

       slotMachine.UpdateUI();

       yield return new WaitForSeconds(3f);
   }

   public float PlayFinalStops()
   {
       int randomIndex = UnityEngine.Random.Range(0, payLineData.lineWins.Count);
       var SymbolWinpair = payLineData.lineWins.ElementAt(randomIndex);

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
}
