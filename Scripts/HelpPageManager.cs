using UnityEngine;
using UnityEngine.UI;

public class HelpPageManager : MonoBehaviour
{
    public Button infoButton;
    public GameObject helpPageBG;
    public GameObject navigationPanel;

    public Image[] dots = new Image[4];

    public GameObject[] helpPages = new GameObject[4];

    public Sprite PageIndicatorOn;
    public Sprite PageIndicatorOff;

    public int helpPageIndex = 0;

    public void OnInfoButtonClick()
    {
        infoButton.interactable = false; 
        helpPageBG.SetActive(true);
        navigationPanel.SetActive(true);
        ShowHelpPage(helpPageIndex);
    }

    public void ShowHelpPage(int pageIndex)
    {
        helpPages[pageIndex].SetActive(true);
        dots[pageIndex].sprite = PageIndicatorOn;
    }

    public void CloseCurrentHelpPage()
    {
        helpPages[helpPageIndex].SetActive(false);
        dots[helpPageIndex].sprite = PageIndicatorOff;
    }

    public void OnClickNextArrowButton()
    {
        CloseCurrentHelpPage();
        helpPageIndex = (helpPageIndex + 1) % helpPages.Length;
        ShowHelpPage(helpPageIndex);
    }

    public void OnClickPrevArrowButton()
    {
        CloseCurrentHelpPage();
        helpPageIndex = (helpPageIndex - 1 + helpPages.Length) % helpPages.Length;
        ShowHelpPage(helpPageIndex);
    }

    public void OnClickCloseButton()
    {
        CloseCurrentHelpPage();
        helpPageBG.SetActive(false);
        navigationPanel.SetActive(false);
        infoButton.interactable = true;
        helpPageIndex = 0;
    }
    
}
