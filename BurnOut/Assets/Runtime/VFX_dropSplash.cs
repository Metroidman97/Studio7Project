using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_dropSplash : MonoBehaviour
{
    [SerializeField] private GameObject splashPrefab;
    [SerializeField] private Transform splashPoint;

    public void PlaySplash()
    {
        if (splashPrefab == null || splashPoint == null)
        {
            Debug.LogWarning("Splash VFX is not configured!");
            return;
        }

        Instantiate(
            splashPrefab,
            splashPoint.position,
            Quaternion.identity
        );
    }
}

