using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public int ID;

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
    }
}
