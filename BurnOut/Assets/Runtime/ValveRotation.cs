using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveRotation : MonoBehaviour
{
    // The angle of rotation in the previous frame
    private float previousZangle;

    // The total amount of degrees rotated
    public float accumulatedDegrees = 0f;

    // Total number of rotations
    public int rotations = 0;

    // Task manager script
    public Task3Manager task;

    // Check if the valve has reached the end of its rotation
    private bool hasReachedEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set the previous angle to 0 on start
        previousZangle = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        TrackRotations();
    }

    private void TrackRotations()
    {
        float currentZangle = transform.eulerAngles.z;

        float deltaAngle = currentZangle - previousZangle;

        if (deltaAngle > 180f)
        {
            deltaAngle -= 360f;
        }
        else if (deltaAngle < -180f)
        {
            deltaAngle += 360f;
        }

        accumulatedDegrees += deltaAngle;

        if (accumulatedDegrees >= 360f)
        {
            if (rotations >= 3)
            {
                transform.rotation = Quaternion.identity;
                if (!hasReachedEnd)
                {
                    // Make it so the coroutine only triggers once
                    task.StartWater();
                    hasReachedEnd = true;
                }
            }
            else
            {
                accumulatedDegrees -= 360f;
                rotations++;
            } 
        }
        else if (accumulatedDegrees <= 0f)
        {
            if (rotations <= 0)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                accumulatedDegrees += 360f;
                rotations--;
            }
        }

        previousZangle = currentZangle;
    }

    public void ResetRotations()
    {
        rotations = 0;
        accumulatedDegrees = 0f;
        hasReachedEnd = false;
    }
}
