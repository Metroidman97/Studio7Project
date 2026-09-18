using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleManager : MonoBehaviour
{

    public Slider[] candles;                    // Array for candle sliders

    private float[] burnSpeeds = new float[4];  // Array for different candle burn timers

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BurnCandles();
    }

    void BurnCandles()
    {
        for (int i = 0;  i < candles.Length; i++)
        {
            candles[i].value = Mathf.MoveTowards(candles[i].value, 0f, burnSpeeds[i] * Time.deltaTime);     // Reduce the differnet candle meters in the top corner
        }
    }

    public void RestoreCandle(float amount, int candleIndex)
    {
        candles[candleIndex].value += amount;   // Restore some of the meter when task is completed
    }

    public void SetBurnSpeeds(float speed, int index)
    {
        burnSpeeds[index] = speed;      // Set the different burn timers
    }
}
