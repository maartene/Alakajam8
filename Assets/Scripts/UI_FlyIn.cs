using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FlyIn : MonoBehaviour
{

    RectTransform rt;
    public float from_x = -500;
    public float from_y = 0;
    Player player;
    Vector2 targetLocation;
    Vector2 fromLocation;
    bool flyingIn = false;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        targetLocation = rt.anchoredPosition;
        fromLocation = new Vector2(from_x, from_y);
        rt.anchoredPosition = fromLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            return;
        }

        if (player.enabled && flyingIn == false)
        {
            StartCoroutine(FlyIn());
        }
    }

    IEnumerator FlyIn(float duration = 0.5f)
    {
        flyingIn = true;
        float t = 0;
        while (t < 1)
        {
            rt.anchoredPosition = Vector2.Lerp(fromLocation, targetLocation, t);
            yield return null;
            t += Time.deltaTime / duration;
        }

        rt.anchoredPosition = targetLocation;
        this.enabled = false;
    }
}
