using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImageChanger : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTask0();
    }

    public void DisplayTask0()
    {
        DisplayTask(0);
    }

    public void DisplayTask1()
    {
        DisplayTask(1);
    }

    public void DisplayTask2()
    {
        DisplayTask(2);
    }

    public void DisplayTask3()
    {
        DisplayTask(3);
    }

    private void DisplayTask(int currentTask) 
    {
        if (thisSprite != null) 
        {
            thisSprite.sprite = backgrounds[currentTask];
        }

    }


    
}
