using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveRotation : MonoBehaviour
{
    // The angle of rotation in the previous frame
    private float previousZangle;

    // The total amount of degrees rotated
    private float accumulatedDegrees = 0f;

    // Total number of rotations
    public int rotations = 0;


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
                Debug.Log("Jammed");
            }
            else
            {
                accumulatedDegrees -= 360f;
                rotations++;
            } 
        }
        else if (accumulatedDegrees <= -360f)
        {
            if (rotations <= 0)
            {
                transform.rotation = Quaternion.identity;
                Debug.Log("Jammed");
            }
            else
            {
                accumulatedDegrees += 360f;
                rotations--;
            }
        }

        previousZangle = currentZangle;
    }
}
