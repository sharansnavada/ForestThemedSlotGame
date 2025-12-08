using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkPayLines : MonoBehaviour
{

    public float blinkInterval = 1f;

    private Image image;

    void OnEnable()
    {
        image = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    void OnDisable()
    {
        StopAllCoroutines();

        if (image != null)
            image.enabled = true; // it makes sure it stays visible when re-enabled
    }

    IEnumerator Blink()
    {
        while (true)
        {
            image.enabled = !image.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
