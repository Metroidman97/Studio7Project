using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

public static class SaveSystem
{
    // Save files for the main game and endless mode. Main game save file only saves the night number, endless mode save file saves the player's high score
    public static string mainFilePath = Path.Combine(Application.persistentDataPath, "mainSaveFile");       // Main game save file
    public static string endlessFilePath = Path.Combine(Application.persistentDataPath, "endlessSaveFile"); // Endless mode save file

    public static void SaveGame(PlayerSaveData data, bool endlessMode)
    {
        string json;

        if (!endlessMode)   // Save the night number if saving the main game
        {
            if (data.nightNum > 0) // Make sure the night number is greater than 0
            {
                json = JsonUtility.ToJson(data.nightNum, true);
                File.WriteAllText(mainFilePath, json);
                Debug.Log($"Game saved to {mainFilePath}");
            }
            else
            {
                Debug.LogError("Invalid night number");
            }
        }
        else if (endlessMode)   // Save the endless mode high score if saving in endless mode
        {
            if (data.endlessTime != float.NaN)
            {
                json = JsonUtility.ToJson(data.endlessTime, true);
                File.WriteAllText(endlessFilePath, json);
                Debug.Log($"Game saved to {endlessFilePath}");
            }
            else
            {
                Debug.LogError("Invalid endless mode score");
            }
        }
        else
        {
            Debug.LogError("Failed to save game data, invalid game state");
        }
    }

    public static PlayerSaveData LoadGame(bool endlessMode)
    {
        string json;
        PlayerSaveData data;

        if (!endlessMode)
        {
            if (!File.Exists(mainFilePath))
            {
                Debug.LogWarning("Main save file not found");
                return null;
            }
            json = File.ReadAllText(mainFilePath);
            data = JsonUtility.FromJson<PlayerSaveData>(json);
            return data;
        }
        else if (endlessMode)
        {
            if (!File.Exists(endlessFilePath))
            {
                Debug.LogWarning("Endless mode save file not found");
                return null;
            }
            json = File.ReadAllText(endlessFilePath);
            data = JsonUtility.FromJson<PlayerSaveData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Failed to load game data, invalid game state");
            return null;
        }
    }
}