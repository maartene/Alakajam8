using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DownInstructions : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        } else
        {
            if (player.enabled && Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(FadeOut());
            }
        }
    }

    IEnumerator FadeOut(float duration = 1f)
    {
        float t = 0;
        while (t < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
            t += Time.deltaTime / duration;
        }

        gameObject.SetActive(false);
    }
}
