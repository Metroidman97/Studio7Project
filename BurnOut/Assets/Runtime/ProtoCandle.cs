using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoCandle : MonoBehaviour
{
    // UI game objects
    public Button protoButton;
    public Slider protoCandleTimer;

    // Burn speed for each individual candle
    public float burnSpeed = 0.1f;

    // Script objects for the various managers
    public CandleManager candleManager;
    public TaskManager taskManager;

    // Amount of meter restored when task is completed
    private float restoreAmount = 0.25f;

    // Enum for different candle ids
    public enum CandleType
    {
        Front,  // Candle 0
        Back,   // Candle 1
        Left,   // Candle 2
        Right   // Candle 3
    }

    // Public enum variable for selecting candles
    public CandleType candleType;

    // Start is called before the first frame update
    void Start()
    {
        candleManager.SetBurnSpeeds(burnSpeed, (int)candleType);    // Set the custom burn speed in the candle manager
    }

    // Update is called once per frame
    void Update()
    {
        CandleBurn();
    }

    void CandleBurn()
    {
        protoCandleTimer.value = candleManager.candles[(int)candleType].value;      // Reduce the main candle meter
    }

    public void RestoreCandle()
    {
        candleManager.RestoreCandle(restoreAmount, (int)candleType);    // Restore some of the meter
    }

    public void SwitchCandle()
    {
        taskManager.SwitchTask((int)candleType);    // Switch to the selected candle
    }
}
