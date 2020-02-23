using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeOut : MonoBehaviour
{

    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut(float duration = 1.0f)
    {
        float t = 0;
        canvasGroup.alpha = 1;
        while (t < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
            t += Time.deltaTime / duration;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
