using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Enum for difficulty states
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Expert
    }

    // Public enum difficulty variable
    public Difficulty difficulty;

    // Difficulty based burn speed modifier
    public float burnSpeedMultiplier = 1f;

    // Difficulty based restore speed modifier
    public float restoreSpeedModifier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetMultiplier();
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
}
