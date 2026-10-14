using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigil : MonoBehaviour
{
    [SerializeField] private GameObject[] sigilPoints;

    private Task1Manager task;


    private void Awake()
    {
        task = GameObject.Find("Task1").GetComponent<Task1Manager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set all but the first point as inactive
        for (int i = 1; i < sigilPoints.Length; i++)
        {
            sigilPoints[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSigilPoint(int id)
    {
        if (id < sigilPoints.Length - 1)
        {
            sigilPoints[id].SetActive(false);
            sigilPoints[id + 1].SetActive(true);
        }
        else if (id == sigilPoints.Length - 1)
        {
            task.FinishTask();
        }
    }
}
