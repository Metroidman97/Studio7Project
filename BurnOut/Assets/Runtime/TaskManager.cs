using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject[] tasks;      // Array of different tasks

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < tasks.Length; i++)
        {
            tasks[i].gameObject.SetActive(false);   // Set all but the first task as inactive
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTask(int taskID)
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            // Set the selected task as active, and deactivate all the other tasks when one of the buttons are pressed
            if (i == taskID)
            {
                tasks[i].gameObject.SetActive(true);
            }
            else
            {
                tasks[i].gameObject.SetActive(false);
            }
        }
    }
}
