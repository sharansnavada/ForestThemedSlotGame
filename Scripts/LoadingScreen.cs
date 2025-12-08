using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject freeGameAdvertise;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DisableLoadingScreen());
    }

    IEnumerator DisableLoadingScreen()
    {
        yield return new WaitForSeconds(4f);
        loadingScreen.SetActive(false);
    }

    public void OnClickContinueBtn()
    {
        freeGameAdvertise.SetActive(false);
    }
}
