using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Manager : MonoBehaviour, ITask
{
    // Public UI elements
    public GameObject releaseButton;
    public GameObject taskUI;

    // Valve object
    public GameObject valve;

    // Candle meter object
    public Candle candle;

    // Game Manager script. Not needed now, but will save it for later
    //private GameManager gameManager;

    private void Awake()
    {
        // Get the game manager script
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Set the release button to inactive
        releaseButton.SetActive(false);

        // Reset the valve's rotation and make it non-interactable
        valve.transform.rotation = Quaternion.identity;
        valve.layer = LayerMask.NameToLayer("Default");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTask()
    {
        valve.layer = LayerMask.NameToLayer("Rotatable");
        valve.GetComponent<ValveRotation>().ResetRotations();
    }

    public void SwitchToTask()
    {
        taskUI.SetActive(true);
    }

    public void SwitchFromTask()
    {
        taskUI.SetActive(false);
    }

    public void StartWater()
    {
        valve.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(FillWater());
    }

    public void FinishTask()
    {
        releaseButton.SetActive(false);
        candle.PutOutOfDanger();
    }

    private IEnumerator FillWater()
    {
        float waitTime = 3f;
        yield return new WaitForSeconds(waitTime);
        releaseButton.SetActive(true);
    }
}
