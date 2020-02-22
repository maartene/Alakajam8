using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public Triangle aboveTriangle;
    public Triangle downTriangle;
    public Triangle leftTriangle;
    public Triangle rightTriangle;
    public int x;
    public int y;

    public bool upsideDown = false;
    public float speed = 4;
    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
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

    private void OnMouseDown()
    {
        if (aboveTriangle != null) Debug.Log("Above triangle:", aboveTriangle.gameObject);
        if (downTriangle != null) Debug.Log("Down triangle:", downTriangle.gameObject);
    }
}
