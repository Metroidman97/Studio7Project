using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Check if the ingredient is in the cauldron
    public bool isInCauldron = false;

    private Task0Manager taskManager;

    private Vector2 startPosition;

    private void Awake()
    {
        taskManager = GameObject.Find("Task0").GetComponent<Task0Manager>();
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cauldron")
        {
            isInCauldron = true;
            taskManager.CheckIngredient(gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cauldron")
        {
            isInCauldron = false;
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
