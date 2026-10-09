using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightManager : MonoBehaviour
{
    public Light directionalLight;

    private float startIntensity = 1.0f;
    private float currentIntensity = 0.5f;

    private Candle candleDisplay;

    // Start is called before the first frame update
    void Start()
    {
        candleDisplay = GameObject.Find("Task0CandleMeter").GetComponent<Candle>();
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.intensity = currentIntensity;
    }

    void SetCurrentIntensity() 
    {
        
    }
}
