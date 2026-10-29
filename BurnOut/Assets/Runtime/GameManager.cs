using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen;

    public GameObject loseScreen;

    // Enum for difficulty states
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    // Counter for the current night. Will be used to select difficulty.
    public int nightCounter;

    // Public enum difficulty variable
    public Difficulty difficulty;

    // Difficulty based burn speed modifier
    public float burnSpeedMultiplier = 1f;

    // Difficulty based restore speed modifier
    public float restoreSpeedModifier = 1f;

    public float nightTimerModifier = 1f;

    public bool debugMode = false;

    public GameObject[] candleMeters;

    // How frequently candles get put in danger automatically
    private float dangerFrequency;

    public bool endlessMode = false;

    // Timer for how long the player lasts in endless mode
    public float endlessTimer = 0;

    // The number of tasks completed during endless mode
    private int endlessTaskCounter = 0;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        // Hide the danger mode button for each candle when debug mode is off
        if (!debugMode)
        {
            for (int i = 0; i < candleMeters.Length; i++)
            {
                candleMeters[i].GetComponent<Candle>().HideDebugButton();
            }

            // Set the frequency to be different based on the difficulty. Set up like this so the difficulty can change in endless mode
            dangerFrequency = SetDangerFrequency();
            InvokeRepeating("PutCandleInDanger", 3f, dangerFrequency);
        }

        if (endlessMode)
        {
            GameObject.Find("NightInfo").GetComponent<NightTimer>().ActivateEndlessMode();
            difficulty = Difficulty.Easy;   // Set the difficulty to easy at the start of endless mode
        }
        SetMultiplier();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PutCandleInDanger()
    {
        int randomIndex = Random.Range(0, candleMeters.Length);

        if (candleMeters[randomIndex].GetComponent<Candle>().GetDangerStatus())     // Check if the candle is already in danger
        {
            // If it is, run the function again
            PutCandleInDanger();
        }
        else
        {
            // If it isn't, put it in danger
            candleMeters[randomIndex].GetComponent<Candle>().PutInDanger();
        }
    }

    private void SetMultiplier()
    {
        switch(difficulty)
        { 
            case Difficulty.Easy:
                burnSpeedMultiplier = 0.5f;
                restoreSpeedModifier = 2f;
                break;
            case Difficulty.Medium:
                burnSpeedMultiplier = 1f;
                restoreSpeedModifier = 1.5f;
                break;
            case Difficulty.Hard:
                burnSpeedMultiplier = 1.5f;
                restoreSpeedModifier = 1f;
                break;
        }
    }

    private float SetDangerFrequency()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return 20f;
            case Difficulty.Medium:
                return 15f;
            case Difficulty.Hard:
                return 10f;
            default:
                return 30f;
        }
    }

    public void LevelWin()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelLose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncrementTaskNumber()
    {
        // Increment the task counter every time one is completed
        endlessTaskCounter++;
        IncreaseDifficulty(endlessTaskCounter);
    }

    private void IncreaseDifficulty(int tasksCompleted)
    {
        switch (tasksCompleted)
        {
            case 5: difficulty = Difficulty.Medium; 
                break;
            case 10: difficulty = Difficulty.Hard;
                break;
        }

        // Increase the difficulty for each task and change the relevant candle modifiers
        IncreaseDifficulty(difficulty);
        SetMultiplier();
    }

    private void IncreaseDifficulty(Difficulty difficulty)
    {
        for (int i = 0; i < candleMeters.Length; i++)
        {
            candleMeters[i].GetComponent<Candle>().ChangeTaskDifficulty((int)difficulty);
        }
    }
}
