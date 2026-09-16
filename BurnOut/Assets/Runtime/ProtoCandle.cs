using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoCandle : MonoBehaviour
{
    public Button protoButton;
    public Slider protoCandleTimer;
    public float burnSpeed = 0.25f;

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
        protoCandleTimer.value = Mathf.MoveTowards(protoCandleTimer.value, 0f, burnSpeed *  Time.deltaTime);
    }

    public void RestoreCandle()
    {
        protoCandleTimer.value += 0.25f;
    }
}
