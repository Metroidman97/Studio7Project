using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private Transform[] cameraPositions;    // Array of camera positions 

    // Start is called before the first frame update
    void Start()
    {
        SwitchTask(0);    // Set the camera's starting position to the first task
    }

    // Look for a way to clean this up
    public void SwitchToTask0()
    {
        SwitchTask(0);
    }

    public void SwitchToTask1()
    {
        SwitchTask(1);
    }

    public void SwitchToTask2()
    {
        SwitchTask(2);
    }

    public void SwitchToTask3()
    {
        SwitchTask(3);
    }

    private void SwitchTask(int taskID)
    {
        for (int i = 0; i < cameraPositions.Length; i++)
        {
            if (i == taskID)
            {
                gameObject.transform.position = cameraPositions[i].position;
                cameraPositions[i].GetComponentInParent<ITask>().SwitchToTask();
            }
            else
            {
                cameraPositions[i].GetComponentInParent<ITask>().SwitchFromTask();
            }
        }
    }
}
