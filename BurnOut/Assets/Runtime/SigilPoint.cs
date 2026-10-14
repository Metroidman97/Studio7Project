using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilPoint : MonoBehaviour
{
    public int id = 0;

    private Sigil sigil;

    private void Awake()
    {
        sigil = GetComponentInParent<Sigil>();
    }

    public void PointTapped()
    {
        sigil.NextSigilPoint(id);
        //Debug.Log("Point Tapped");
    }
}
