using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilPoint : MonoBehaviour
{
    // ID number for the point, to determine the order they are pressed
    public int id = 0;

    private Sigil sigil;

    private void Awake()
    {
        sigil = GetComponentInParent<Sigil>();
    }

    public void PointTapped()
    {
        sigil.NextSigilPoint(id);
    }
}
