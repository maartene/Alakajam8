using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StartGameButton : MonoBehaviour
{

    public CanvasGroup titleScreen;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCR());
    }

    IEnumerator StartGameCR(float duration = 0.5f)
    {
        float t = 0;
        while (t < 1)
        {
            titleScreen.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
            t += Time.deltaTime / duration;
        }

        player.enabled = true;
        titleScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.enabled = false;
            }       
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(StartGameCR());
            }
        }
    }
}
