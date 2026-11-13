using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

public static class SaveSystem
{
    // Save files for the main game and endless mode. Main game save file only saves the night number, endless mode save file saves the player's high score
    public static string mainFilePath = Path.Combine(Application.persistentDataPath, "mainSaveFile.txt");       // Main game save file
    public static string endlessFilePath = Path.Combine(Application.persistentDataPath, "endlessSaveFile.txt"); // Endless mode save file

 
    public static void SaveMainGame(int nightNum)
    {
        if (nightNum > 0)
        {
            File.WriteAllText(mainFilePath, nightNum.ToString());
            Debug.Log($"Game saved to {mainFilePath}");
        }
        else
        {
            Debug.LogError("Invalid night number");
        }
    }

    public static void SaveEndlessHiScore(float endlessScore)
    {
        if (endlessScore != float.NaN)
        {
            File.WriteAllText(endlessFilePath, endlessScore.ToString());
            Debug.Log($"Game saved to {endlessFilePath}");
        }
        else
        {
            Debug.LogError("Invalid endless mode score");
        }
    }

    public static int LoadMainGame()
    {
        if (!File.Exists(mainFilePath))
        {
            Debug.LogWarning("Main save file not found");
            return 0;
        }
        int.TryParse(File.ReadAllText(mainFilePath), out int result);
        return result;
    }

    public static float LoadEndlessHiScore()
    {
        if (!File.Exists(endlessFilePath))
        {
            Debug.LogWarning("Endless mode save file not found");
            return float.NaN;
        }
        float.TryParse(File.ReadAllText(endlessFilePath), out float result);
        return result;
    }
}