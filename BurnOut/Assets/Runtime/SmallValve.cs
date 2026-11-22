using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallValve : MonoBehaviour, ITouchPoint
{
    private Task3Manager task;
    private AudioSource audioSource;
    [SerializeField] private AudioClip valveSound;

    private float rotationAngle = 360f;
    private float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        task = GetComponentInParent<Task3Manager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointTapped()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(ValveRotate());
        StartCoroutine(PlayValveSqueak());
    }

    private IEnumerator ValveRotate()
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + rotationAngle;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentZ = Mathf.Lerp(startRotation, endRotation, elapsed/duration);
            transform.eulerAngles = new Vector3(0f, 0f, currentZ);
            yield return null;
        }

        // Have the valve register as done only after it stops spinning
        task.SmallValveTurn();
        transform.eulerAngles = new Vector3 (0f, 0f, endRotation);
    }
    private IEnumerator PlayValveSqueak()
    {
        audioSource.PlayOneShot(valveSound);
        yield return new WaitForSeconds(0.9f);
        audioSource.Stop();
    }
}
