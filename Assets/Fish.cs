using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    public enum Direction {
        Left,
        Right
    }

    public int x;
    public int y;

    public float speed = 2;
    public Direction direction { get; private set; }

    private void Start()
    {
        SetDirection(Direction.Right);
    }

    public void SetTargetLocation(Triangle triangle)
    {
        x = triangle.x;
        y = triangle.y;
        StartCoroutine(MoveTo(triangle.transform.position));
    }

    public void SetDirection(Direction dir)
    {
        this.direction = dir;
        if (dir == Direction.Right)
        {
            transform.localScale = Vector3.one;
        } else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    IEnumerator MoveTo(Vector3 targetPosition)
    {
        Vector3 originalPosition = transform.position;
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            yield return null;
            t += Time.deltaTime * speed;
        }

        transform.position = targetPosition;
    }

    public IEnumerator FadeIn(float duration)
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        Color originalColor = sr.color;
        Color fadeInColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float t = 0;
        while (t < 1)
        {
            sr.color = Color.Lerp(fadeInColor, originalColor, t);
            yield return null;
            t += Time.deltaTime / duration;
        }
        sr.color = originalColor;
    }
}
