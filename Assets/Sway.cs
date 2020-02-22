using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{

    public float hSpeed = 0;
    public float vSpeed = 0.1f;
    public float vAmplitude = 0.1f;
    public float hAmplitude = 0;

    float timeOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeOffset = Random.Range(0, 2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(hAmplitude * Mathf.Sin(hSpeed * Time.time), vAmplitude * Mathf.Sin(vSpeed * Time.time + timeOffset), 0);
    }
}
