using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public int ID;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer sprites;

    [SerializeField]
    private Transform[] snapPoints;
    [SerializeField]
    private float snapThreshold = 1.5f;
    [SerializeField]
    private float snapSpeed = 10f;

    private Task2Manager task;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        task = GameObject.Find("Task2").GetComponent<Task2Manager>();
    }

    public void SnapToNearestPoint()
    {
        Transform closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform point in snapPoints)
        {
            if (point == null) continue;

            float dist = Vector2.Distance(transform.position, point.position);

            if (dist < closestDistance && dist <= snapThreshold)
            {
                closestDistance = dist;
                closestPoint = point;
            }
        }
        
        if (closestPoint != null)
        {
            StartCoroutine(SmoothSnapTo(closestPoint.position));
        }
    }

    private IEnumerator SmoothSnapTo(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, snapSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        task.CheckBones();
        PlayAnimations(task.NewCorrectBone());
      
    }

    //JACOB
    private void PlayAnimations(bool correct)
        //bool correct
    {
        sprites.enabled = true;
        animator.enabled = true;
        //animator.Play("bonesFail", 0, 0f);

        print(correct);
        if (correct) { animator.Play("bonesSucess", -1, 0f); }
        else { animator.Play("bonesFail", -1, 0f); }

        StartCoroutine(DelayedHide());
    }

    IEnumerator DelayedHide()
    { 
        yield return new WaitForSeconds(0.85f);
        sprites.enabled = false;
        animator.enabled = false;
    }
}
