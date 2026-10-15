using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigil : MonoBehaviour
{
    [SerializeField] private GameObject[] sigilPoints;

    private Task1Manager task;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        task = GameObject.Find("Task1").GetComponent<Task1Manager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set all but the first point as inactive
        ResetPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSigilPoint(int id)
    {
        // If the tapped point is not the last point, move to the next point, else finish the task
        if (id < sigilPoints.Length - 1)
        {
            sigilPoints[id].SetActive(false);
            sigilPoints[id + 1].SetActive(true);

            // Make the color fade in with each point tapped
            SetSpriteAlpha(((float)id + 1f) / (float)sigilPoints.Length);
        }
        else if (id == sigilPoints.Length - 1)
        {
            task.FinishTask();
        }
    }

    public void ResetPoints()
    {
        // Set the first point active and deactivate the rest
        sigilPoints[0].SetActive(true);
        for (int i = 1; i < sigilPoints.Length; i++)
        {
            sigilPoints[i].SetActive(false);
        }

        // Make the color of the sigil completely transparent
        SetSpriteAlpha(0f);
    }

    private void SetSpriteAlpha(float alphaValue)
    {
        Color tempColor = spriteRenderer.color;

        tempColor.a = alphaValue;

        spriteRenderer.color = tempColor;
    }
}
