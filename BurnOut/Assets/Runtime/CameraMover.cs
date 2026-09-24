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
        gameObject.transform.position = cameraPositions[0].position;    // Set the camera's starting position to the first task
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Look for a way to clean this up
    public void SwitchToTask0()
    {
        gameObject.transform.position = cameraPositions[0].position;
    }

    public void SwitchToTask1()
    {
        gameObject.transform.position = cameraPositions[1].position;
    }

    public void SwitchToTask2()
    {
        gameObject.transform.position = cameraPositions[2].position;
    }

    public void SwitchToTask3()
    {
        gameObject.transform.position = cameraPositions[3].position;
    }
}
