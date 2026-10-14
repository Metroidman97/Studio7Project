using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Manager : MonoBehaviour, ITask
{
    // UI elements
    public GameObject taskUI;
    public GameObject finishButton;

    public Candle candle;

    // Arrays of the different sigils for each difficulty
    public GameObject[] easySigils;
    public GameObject[] mediumSigils;
    public GameObject[] hardSigils;

    private GameManager gameManager;
    private int diffRating;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        diffRating = (int)gameManager.difficulty;

        finishButton.SetActive(false);

        // Deactivate all of the sigils
        for (int i = 0;  i < easySigils.Length; i++)
        {
            easySigils[i].SetActive(false);
            mediumSigils[i].SetActive(false);
            hardSigils[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartTask()
    {
        int index = Random.Range(0, easySigils.Length);

        switch (diffRating)
        {
            case 0:
                easySigils[index].SetActive(true);
                easySigils[index].GetComponent<Sigil>().ResetPoints();
                break;
            case 1:
                mediumSigils[index].SetActive(true);
                mediumSigils[index].GetComponent<Sigil>().ResetPoints();
                break;
            case 2:
                hardSigils[index].SetActive(true);
                hardSigils[index].GetComponent<Sigil>().ResetPoints();
                break;
        }
    }

    public void SwitchToTask()
    {
        taskUI.SetActive(true);
    }

    public void SwitchFromTask()
    {
        taskUI.SetActive(false);
    }

    public void FinishTask()
    {
        for (int i = 0; i < easySigils.Length; i++)
        {
            easySigils[i].SetActive(false);
            mediumSigils[i].SetActive(false);
            hardSigils[i].SetActive(false);
        }
        candle.PutOutOfDanger();
    }
}
