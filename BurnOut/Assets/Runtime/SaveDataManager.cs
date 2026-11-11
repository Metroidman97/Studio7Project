using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    // SaveDataManager singleton
    public static SaveDataManager Instance { get; private set; }

    // Default night number is 1, which is for night 1
    public int nightNumber = 1;

    // By default, game is not a new game. If new game is true, do not load save file
    public bool newGame = false;

    // By default, endless mode is disabled
    public bool endlessMode = false;

    private void Awake()
    {
        // Enforce singelton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
