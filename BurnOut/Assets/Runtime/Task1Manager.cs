using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Manager : MonoBehaviour, ITask
{
    public GameObject taskUI;
    public GameObject finishButton;

    public Candle candle;

    public GameObject easySigil;
    public GameObject mediumSigil;
    public GameObject hardSigil;

    private GameManager gameManager;
    private int diffRating;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        diffRating = (int)gameManager.difficulty;

        finishButton.SetActive(false);
        //easySigil.SetActive(false);
        //mediumSigil.SetActive(false);
        //hardSigil.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartTask()
    {
        //finishButton.SetActive(true);
        switch (diffRating)
        {
            case 0:
                easySigil.SetActive(true);
                break;
            case 1:
                mediumSigil.SetActive(true);
                break;
            case 2:
                hardSigil.SetActive(true);
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
        //finishButton.SetActive(false);
        easySigil.SetActive(false);
        mediumSigil.SetActive(false);
        hardSigil.SetActive(false);
        candle.PutOutOfDanger();
    }
}
