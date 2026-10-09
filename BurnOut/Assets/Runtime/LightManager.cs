using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class LightManager : MonoBehaviour
{
    public Light directionalLight;

    private float startIntensity = 1.0f;
    private float currentIntensity = 0.5f;

    public Candle[] candles;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.intensity = SetCurrentIntensity();
    }

    float SetCurrentIntensity() 
    {
        float lowestIntensity = 1.0f;
        for (int i = 0; i < candles.Length; i++)
        {
            if(candles[i].candleMeter.fillAmount < lowestIntensity)
            {
                currentIntensity = candles[i].candleMeter.fillAmount;
            } 
        }
        return currentIntensity;
    }


}
