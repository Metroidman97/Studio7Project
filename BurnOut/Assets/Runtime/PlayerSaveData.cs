using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public int nightNum;
    public float endlessTime;

    // Only use this constructor when saving the main game
    public PlayerSaveData (int nightNumber)
    {
        nightNum = nightNumber;
    }

    // Only use this constructor when saving the endless mode high score
    public PlayerSaveData (float endlessHighScore)
    {
        endlessTime = endlessHighScore;
    }
}
