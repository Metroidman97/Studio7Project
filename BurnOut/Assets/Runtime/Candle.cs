using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Candle : MonoBehaviour
{
    // Meter image
    public Image candleMeter;

    // Base burn speed
    public float burnSpeed = 0.1f;

    // Task manager script, looking for ITask interface
    public GameObject taskObject;

    // ITask interface
    private ITask task;

    // Bool for determining if candle is in danger
    private bool inDanger = false;

    // GameManager script
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   // Get the game manager script
        task = taskObject.GetComponent<ITask>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inDanger)   // Empty the meter when candle is in danger
        {
            BurnCandle();
        }
        else            // Else, restore the candle when out of danger
        {
            RestoreCandle();
        }

        if (candleMeter.fillAmount == 0f)
        {
            gameManager.LevelLose();
        }
    }

    void BurnCandle()
    {
        candleMeter.fillAmount = Mathf.MoveTowards(candleMeter.fillAmount, 0f, burnSpeed * gameManager.burnSpeedMultiplier * Time.deltaTime);
    }

    void RestoreCandle()
    {
        candleMeter.fillAmount = Mathf.MoveTowards(candleMeter.fillAmount, 1f, burnSpeed * gameManager.restoreSpeedModifier * Time.deltaTime);
    }

    public void PutInDanger()
    {
        inDanger = true;
        task.StartTask();
    }

    public void PutOutOfDanger()
    {
        inDanger = false;
    }
}
