using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightTimer : MonoBehaviour
{

    public Slider nightTimer;

    public float nightTimeSpeed = 0.1f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        nightTimer.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressTime();
        if (nightTimer.value == nightTimer.maxValue)
        {
            gameManager.LevelWin();
        }
    }

    private void ProgressTime()
    {
        nightTimer.value = Mathf.MoveTowards(nightTimer.value, 1f, nightTimeSpeed * Time.deltaTime);
    }

    public void EndNight()
    {
        nightTimer.value = nightTimer.maxValue;
    }
}
