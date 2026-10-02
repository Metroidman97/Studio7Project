using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Manager : MonoBehaviour, ITask
{
    public GameObject taskUI;
    public GameObject finishButton;

    public Candle candle;

    // Start is called before the first frame update
    void Start()
    {
        finishButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartTask()
    {
        finishButton.SetActive(true);
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
        finishButton.SetActive(false);
        candle.PutOutOfDanger();
    }
}
