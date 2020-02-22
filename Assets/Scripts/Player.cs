using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    public float speed = 3;
    public int vision = 1;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    GameManager gameManager;
    Sway sway;

    bool isMoving = false;

    void ShowHideArrows()
    {
        if (y == 0)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            upArrow.SetActive(false);
            downArrow.SetActive(true);
            return;
        }

        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        downArrow.SetActive(x % 2 == 0);
        upArrow.SetActive(x % 2 != 0);

        if (y == 1)
        {
            upArrow.SetActive(false);
        }

        if (y >= gameManager.depth - 1)
        {
            downArrow.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        ShowHideArrows();
        sway = GetComponentInChildren<Sway>();
    }

    // Update is called once per frame
    void Update()
    {

        // if we're in the water, and sway is not active, activate sway.
        if (y > 0 && sway.enabled == false)
        {
            sway.enabled = true;
        }

        if (isMoving) { return; }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (x % 2 == 0)
            {
                if (gameManager.CreateTriangle(x + 1, y + 1, vision))
                {
                    x += 1;
                    y += 1;
                    StartCoroutine(MoveTo(gameManager.getTriangle(x, y).transform.position));
                }
            }
            gameManager.Step();
            ShowHideArrows();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gameManager.CreateTriangle(x - 1, y, vision))
            {
                x -= 1;
                StartCoroutine(MoveTo(gameManager.getTriangle(x, y).transform.position));
            }
            gameManager.Step();
            ShowHideArrows();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gameManager.CreateTriangle(x + 1, y, vision))
            {
                x += 1;
                StartCoroutine(MoveTo(gameManager.getTriangle(x, y).transform.position));
            }
            gameManager.Step();
            ShowHideArrows();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (x % 2 == 1 || x % 2 == -1)
            {
                if (gameManager.CreateTriangle(x - 1, y - 1, vision))
                {
                    x -= 1;
                    y -= 1;
                    StartCoroutine(MoveTo(gameManager.getTriangle(x, y).transform.position));
                }
            }
            gameManager.Step();
            ShowHideArrows();
        }
    }

    IEnumerator MoveTo(Vector3 targetPosition)
    {
        isMoving = true;
        Vector3 originalPosition = transform.position;
        float t = 0;
        while (t < 1) {
            transform.position = Vector3.Slerp(originalPosition, targetPosition, t);
            yield return null;
            t += Time.deltaTime * speed;
        }
        transform.position = targetPosition;
        isMoving = false;
    }
}
