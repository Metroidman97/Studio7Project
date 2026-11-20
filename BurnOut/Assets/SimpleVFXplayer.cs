using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVFXplayer : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator animator;
    [SerializeField]
    float waitTime;

    public bool oneTime;

    //[SerializeField]
    //AnimationClip AnimationName;

    [SerializeField]
    string  AnimationName;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        animator.enabled = false;
        //PlayAnimations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimations()
    //bool correct
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
        //animator.Play("bonesFail", 0, 0f);

        print(AnimationName);

        animator.Play(AnimationName, 0, 0f);

        StartCoroutine(DelayedHide());
    }

    IEnumerator DelayedHide()
    {
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.enabled = false;
        animator.enabled = false;
        if (oneTime) 
        { 
            Destroy(gameObject);
        }
        //animator.StopPlayback();
        

    }
}
