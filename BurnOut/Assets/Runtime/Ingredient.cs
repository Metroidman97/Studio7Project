using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // Check if the ingredient is in the cauldron
    public bool isInCauldron = false;

    // Taskmanager script
    private Task0Manager taskManager;

    // Starting position of the ingredient in the scene
    private Vector2 startPosition;

    private void Awake()
    {
        taskManager = GameObject.Find("Task0").GetComponent<Task0Manager>();    // Get the taskmanager script
        startPosition = transform.position;                                     // Record the starting position
        gameObject.layer = LayerMask.NameToLayer("Draggable");                  // Set the layer to draggable, for saftey
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cauldron")
        {
            isInCauldron = true;
            taskManager.CheckIngredient(gameObject);
            gameObject.layer = LayerMask.NameToLayer("Default");    // Set the layer to defualt so the item can't be pulled out of the cauldron
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cauldron")
        {
            isInCauldron = false;
            gameObject.layer = LayerMask.NameToLayer("Draggable");  // Set the object back to draggable
        }
    }

    public void ResetPosition()
    {
        // Reset the object's position and velocity and make it draggable again
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        gameObject.layer = LayerMask.NameToLayer("Draggable");
    }
}
