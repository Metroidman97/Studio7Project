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
        Hard,
        Expert
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

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMultiplier();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
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
            case Difficulty.Expert:
                burnSpeedMultiplier = 2f;
                restoreSpeedModifier = 0.5f;
                break;
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
}
