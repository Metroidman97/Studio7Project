using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Check if the ingredient is in the cauldron
    public bool isInCauldron = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cauldron")
        {
            isInCauldron = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cauldron")
        {
            isInCauldron = false;
        }
    }
}
