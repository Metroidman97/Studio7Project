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
    public int nightCounter = 1;

    private int activeTasks = 1;

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

    public bool newGame = false;

    // Timer for how long the player lasts in endless mode
    public float endlessTimer = 0;

    public float endlessHighScore;

    // The number of tasks completed during endless mode
    private int endlessTaskCounter = 0;

    private SaveDataManager saveDataManager;

    private bool gameIsGoing = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (SaveDataManager.Instance != null)
        {
            saveDataManager = SaveDataManager.Instance;
        }

        if (saveDataManager != null)
        {
            // Set the data from the save data manager
            newGame = saveDataManager.newGame;
            endlessMode = saveDataManager.endlessMode;
            nightCounter = saveDataManager.nightNumber;
        }
        else
        {
            // Default to new game if save data manager is null
            newGame = true;
            endlessMode = false;
            nightCounter = 1;
        }

        if (!newGame && !endlessMode)
        {
            // Only load data when coming from the main menu
            //nightCounter = LoadData();
        }
        else if (newGame && !endlessMode)
        {
            nightCounter = 1;
        }

        if (endlessMode)
        {
            GameObject.Find("NightInfo").GetComponent<NightTimer>().ActivateEndlessMode();
            difficulty = Difficulty.Easy;   // Set the difficulty to easy at the start of endless mode
            endlessHighScore = LoadEndlessScore();
        }
        else if (!endlessMode)
        {
            activeTasks = SetNightDifficulty();   // Set the difficulty on the specific night when not playing in endless mode
            //IncreaseDifficulty(difficulty);
        }
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
            if (activeTasks > 0)
            {
                InvokeRepeating(nameof(PutCandleInDanger), 3f, dangerFrequency);
            }
        }

        SetMultiplier();

        gameIsGoing = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PutCandleInDanger()
    {
        int randomIndex = Random.Range(0, activeTasks);

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

    private int SetNightDifficulty()
    {
        switch(nightCounter)
        {
            case 1:
                difficulty = Difficulty.Easy;
                return 1; // Only the cauldron task is active for night 1
            case 2:
                difficulty = Difficulty.Easy;
                return 2; // Cauldron and sigils are active for night 2
            case 3:
                difficulty = Difficulty.Medium;
                return 3; // Cauldron, sigils, and bones are active night 3
            case 4:
                difficulty = Difficulty.Medium;
                return 4; // All 4 tasks are active night 4
            case 5:
                difficulty = Difficulty.Hard;
                return 4; // All 4 tasks are active night 5
            case 6:
                difficulty = Difficulty.Easy;
                return 0; // No tasks available on night 6, night 6 is the "win screen"
            default:
                Debug.Log(nightCounter);
                Debug.LogWarning("Invalid night number");
                nightCounter = 1;
                difficulty = Difficulty.Easy;
                return 1; // Set to night 1 on invalid night number
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
        switch(difficulty)
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
        if (gameIsGoing)
        {
            SaveData(); // Save the data before incrementing the night counter, so it doesn't go above 5
            saveDataManager.nightNumber++;
            if (saveDataManager.nightNumber >= 6)
            {
                saveDataManager.nightNumber = 6;   // Make sure the night counter doesn't go above 6. Night 6 would be the "win screen"
            }
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
        gameIsGoing = false;    // Only run the code above once
    }

    public void LevelLose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        // Include logic for winning the game if the night counter reaches night 6
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

    private int LoadData()
    {
        PlayerSaveData loadedData = SaveSystem.LoadGame(endlessMode);

        if (loadedData != null)
        {
            return loadedData.nightNum;
        }
        else
        {
            // Default to 1 if no data to load
            return 1;
        }
    }

    private float LoadEndlessScore()
    {
        PlayerSaveData loadedData = SaveSystem.LoadGame(endlessMode);

        if (loadedData != null)
        {
            return loadedData.endlessTime;
        }
        else
        {
            // Default to 0 if no data to load
            return 0;
        }
    }

    public void SaveData()
    {
        if (!endlessMode)
        {
            PlayerSaveData data = new PlayerSaveData(nightCounter);
            SaveSystem.SaveGame(data, endlessMode);
        }
        else if (endlessMode)
        {
            PlayerSaveData data = new PlayerSaveData(endlessHighScore);
            SaveSystem.SaveGame(data, endlessMode);
        }
    }
}
