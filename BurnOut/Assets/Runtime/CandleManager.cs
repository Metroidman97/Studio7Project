using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleManager : MonoBehaviour
{

    public Slider[] candles;

    public float burnSpeed = 0.1f;

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
            candles[i].value = Mathf.MoveTowards(candles[i].value, 0f, burnSpeed * Time.deltaTime);
        }
    }

    public void RestoreCandle(float amount, int candleIndex)
    {
        candles[candleIndex].value += amount;
    }
}
