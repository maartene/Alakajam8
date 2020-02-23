using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DepthLight : MonoBehaviour
{
    public float targetIntensity = 0.5f;
    Light2D m_light;

    // Start is called before the first frame update
    void Start()
    {
        m_light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_light.intensity = Mathf.Lerp(m_light.intensity, targetIntensity, 4.0f * Time.deltaTime);
    }
}
