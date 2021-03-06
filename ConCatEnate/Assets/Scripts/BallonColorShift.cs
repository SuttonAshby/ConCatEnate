using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonColorShift : MonoBehaviour
{
    Color lerpedColor = Color.blue;
    Renderer balloonRenderer;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        balloonRenderer = gameObject.GetComponent<Renderer>();
        lerpedColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / (float)GameManager.Instance.chargeDuration;
        lerpedColor = Color.Lerp(Color.blue, Color.red, t);
        balloonRenderer.material.SetColor("_Color", lerpedColor);
    }
}
