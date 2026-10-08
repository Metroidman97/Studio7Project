using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class Task2Manager : MonoBehaviour, ITask
{
    // Public UI elements
    public GameObject taskUI;
    public GameObject finishButton;

    // Candle meter
    public Candle candle;

    [SerializeField]
    private GameObject[] bones;                             // Array of bone objects

    [SerializeField]
    private GameObject[] pannelBones;                       // Array of bone objects for the pannel

    [SerializeField]
    private Transform[] bonePositionTransforms;             // The positions for the bones

    [SerializeField]
    private Transform[] pannelBonePositionsTransforms;      // The positions for the pannel bones

    private Vector2[] bonePositions = new Vector2[4];       // Array of position vectors for the bones

    private Vector2[] pannelBonePositions = new Vector2[4]; // Array of position vectors for the pannel bones

    // Start is called before the first frame update
    void Start()
    {
        finishButton.SetActive(false);
        SetBonePositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTask()
    {
        // Make the bones draggable
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].gameObject.layer = LayerMask.NameToLayer("Draggable");
        }

        RandomizePannelBones();
    }

    public void SwitchToTask()
    {
        taskUI.SetActive(true);
    }

    public void SwitchFromTask()
    {
        taskUI.SetActive(false);
    }

    public void FinishTask()
    {
        finishButton.SetActive(false);
        candle.PutOutOfDanger();

        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].gameObject.layer = LayerMask.NameToLayer("Default");
        }

        for (int j = 0; j < pannelBonePositionsTransforms.Length; j++)
        {
            pannelBones[j].SetActive(false);
        }
    }

    private void SetBonePositions()
    {
        // Set the starting positions for each bone, and set the pannel bones to invisible
        for (int i = 0; i < bonePositionTransforms.Length; i++)
        {
            bonePositions[i] = bonePositionTransforms[i].position;
            bones[i].transform.position = bonePositions[i];
        }

        for (int j = 0; j < pannelBonePositionsTransforms.Length; j++)
        {
            pannelBonePositions[j] = pannelBonePositionsTransforms[j].position;
            pannelBones[j].transform.position = pannelBonePositions[j];
            pannelBones[j].SetActive(false);
        }
    }

    private void RandomizePannelBones()
    {
        // Make the pannel bones visible and shuffle their positions
        pannelBonePositions.Shuffle();
        for (int i = 0; i < pannelBones.Length; i++)
        {
            pannelBones[i].SetActive(true);
            pannelBones[i].transform.position = pannelBonePositions[i];
        }
    }

    public void CheckBones()
    {
        int correctBones = 0;

        for (int i = 0; i < bones.Length; i++)
        {
            Vector2 bonePos = new Vector2((float)Math.Round(bones[i].transform.localPosition.x, 2), (float)Math.Round(bones[i].transform.localPosition.y, 2));
            Vector2 pannelBonePos = new Vector2((float)Math.Round(pannelBones[i].transform.localPosition.x, 2), (float)Math.Round(pannelBones[i].transform.localPosition.y, 2));

            if (bonePos == pannelBonePos)
            {
                correctBones++;
            }
            Debug.Log(correctBones);
        }

        if (correctBones == 4)
        {
            finishButton.SetActive(true);
        }
    }
}
