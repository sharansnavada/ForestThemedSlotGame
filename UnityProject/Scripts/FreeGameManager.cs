using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeGameManager : MonoBehaviour
{
    float totalFreeGameWin = 0;
    public TextMeshProUGUI amountWon;
    public GameObject BigWinBanner;

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

    public GameObject winText;

    public bool isFirstGame = true;

    public SoundManager soundManager;

    void Start()
    {
        
    }

    void Update()
    {

    }
    private IEnumerator RunOneGame()
    {
        foreach (GameObject line in matchingLines)
        {
            line.SetActive(false);
        }

        // wait for a single spin to finish
        yield return StartCoroutine(Spin());
    }

    public void OnDoubleClickSpinBtn()
    {
        soundManager.reelSpinAudio.Play();

        spinsPlayed.text = "0";

        StartCoroutine(FreeGamesFlow());

        totalFreeGameWin = 0;

        isFirstGame = true;

    }

    private IEnumerator FreeGamesFlow()
    {
        
        yield return StartCoroutine(RunOneGame());

        soundManager.reelSpinAudio.Pause();
        soundManager.bonusLandStopAudio.Play();

        soundManager.baseGameAudio.Pause();
        soundManager.bonusWinAudio.Play();

        isFirstGame = false;
        // 1) Show grey BG + text for 3 seconds
        yield return StartCoroutine(EnableBGandFGText());

        // 2) Enter free game UI state
        EnableFreeGameUI();

        // 3) Play all free spins
        yield return StartCoroutine(PlayFreeGamesRoutine());

        // 4) Back to base game UI
        RevertFreeGameUI();

        yield return StartCoroutine(PlayBigWinAnimation());

    }

    IEnumerator EnableBGandFGText()
    {
        greyBackGround.SetActive(true);
        FreeGameTextImage.SetActive(true);

        yield return new WaitForSeconds(8f);

        greyBackGround.SetActive(false);
        FreeGameTextImage.SetActive(false);
    }

    public void EnableFreeGameUI()
    {
        // cahnging the background
        SpriteRenderer bgRenderer = BaseGameBackGroundGameObject.GetComponent<SpriteRenderer>();
        if (bgRenderer != null)
        {
            // store current sprite, just in case you need it
            baseGameBG = bgRenderer.sprite;
            bgRenderer.sprite = freeGameImage;
        }

        gameLogo.SetActive(false);
        freeSpin_Banner.SetActive(true);

        DisableButtonPanels();

        spinsDataGameObject.SetActive(true);

        winText.SetActive(false);
    }

    public void RevertFreeGameUI()
    {
        // Revert SpriteRenderer sprite back to base game
        SpriteRenderer bgRenderer = BaseGameBackGroundGameObject.GetComponent<SpriteRenderer>();
        if (bgRenderer != null)
        {
            bgRenderer.sprite = BaseGameImage;
        }

        gameLogo.SetActive(true);
        freeSpin_Banner.SetActive(false);

        EnableButtonPanels();

        spinsDataGameObject.SetActive(false);

        winText.SetActive(true);
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
        foreach (GameObject line in matchingLines)
        {
            line.SetActive(false);
        }

        // wait for a single spin to finish
        freeGameReelSpinCount = 0;

        while (freeGameReelSpinCount < 8)
        {
            foreach (GameObject line in matchingLines)
            {
                line.SetActive(false);
            }

            // wait for a single spin to finish
            yield return StartCoroutine(Spin());

            freeGameReelSpinCount++;
            spinsPlayed.text = freeGameReelSpinCount.ToString();

            soundManager.bonusWinAudio.volume = 0.3f;
        }

        amountWon.text = "$" + totalFreeGameWin.ToString();
    }

    IEnumerator Spin()
    {
        soundManager.bonusWinAudio.volume = 0.3f;
        soundManager.reelSpinAudio.Play();

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

        winAmount += PlayFinalStops();

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
        int randomIndex = 0;

        if (isFirstGame)
            randomIndex = 14;
        else
            randomIndex = UnityEngine.Random.Range(0, payLineData.lineWins.Count);

        var SymbolWinpair = payLineData.lineWins.ElementAt(randomIndex);

        List<string> payLineString = SymbolWinpair.Key;

        for (int i = 0; i < 15; i++)
        {
            currentSymbol = payLineString[i];
            SymbolData bonusSymbol = allSymbols.Find(s => s.name == currentSymbol);
            currentSymbols[i] = bonusSymbol;
            reelImages[i].sprite = currentSymbols[i].symbolSprite;
        }

        soundManager.reelSpinAudio.Pause();
        soundManager.bonusLandStopAudio.Play();

        soundManager.bonusWinAudio.volume = 1f;

        matchingLines[randomIndex].SetActive(true);

        totalFreeGameWin += SymbolWinpair.Value * slotMachine.bet;
        return SymbolWinpair.Value * slotMachine.bet;
    }

    IEnumerator PlayBigWinAnimation()
    {

        soundManager.bonusWinAudio.Pause();

        soundManager.bigWinAnimationAudio.Play();

        BigWinBanner.SetActive(true);
        yield return new WaitForSeconds(8f);
        BigWinBanner.SetActive(false);

        soundManager.bigWinAnimationAudio.Pause();

        soundManager.baseGameAudio.Play();

    }
}



