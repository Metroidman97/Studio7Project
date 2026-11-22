using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallValve : MonoBehaviour, ITouchPoint
{
    private Task3Manager task;
    // Start is called before the first frame update
    void Start()
    {
        task = GetComponentInParent<Task3Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointTapped()
    {
        //Debug.Log("Valve turning");
        gameObject.layer = LayerMask.NameToLayer("Default");
        task.SmallValveTurn();
    }
}
