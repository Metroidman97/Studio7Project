using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoCandle : MonoBehaviour
{
    public Button protoButton;
    public Slider protoCandleTimer;
    public float burnSpeed = 0.1f;

    public CandleManager candleManager;
    public TaskManager taskManager;

    private float restoreAmount = 0.25f;

    public enum CandleNum
    {
        Front,  // Candle 0
        Back,   // Candle 1
        Left,   // Candle 2
        Right   // Candle 3
    }

    public CandleNum candleNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CandleBurn();
    }

    void CandleBurn()
    {
        protoCandleTimer.value = candleManager.candles[(int)candleNum].value;
    }

    public void RestoreCandle()
    {
        candleManager.RestoreCandle(restoreAmount, (int)candleNum);
    }

    public void SwitchCandle()
    {
        taskManager.SwitchTask((int)candleNum);
    }
}
