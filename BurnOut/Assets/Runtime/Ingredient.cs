using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ingredient : MonoBehaviour
{
    // Check if the ingredient is in the cauldron
    public bool isInCauldron = false;

    // Taskmanager script
    private Task0Manager taskManager;

    // Starting position of the ingredient in the scene
    private Vector2 startPosition;

    //JACOB
    private PolygonCollider2D collisionOn2DPlane;

    private void Awake()
    {
        taskManager = GameObject.Find("Task0").GetComponent<Task0Manager>();    // Get the taskmanager script
        startPosition = transform.position;                                     // Record the starting position
        gameObject.layer = LayerMask.NameToLayer("Default");                    // Set the layer to default so they can't be dragged
        
        //JACOB START
        collisionOn2DPlane = gameObject.GetComponent<PolygonCollider2D>();
        //JACOB END
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
            //gameObject.layer = LayerMask.NameToLayer("Draggable");  // Set the object back to draggable
        }
    }

    public void ResetPosition()
    {
        // Reset the object's position and velocity and make it undraggable again
        RevertBack();
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        ;
    }

    //JACOB START: Sets "Shelf" layer as exluded for 2D collider. Moves ingredient closer to camera, over the shelf models.
    public void BringForward()
    {
        collisionOn2DPlane.excludeLayers = LayerMask.GetMask("Shelf");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
    }

    //Dropping an ingredient clears the excluded layers before re-endabling "draggable" mask. This is to avoid ingredients colliding into each other.
    public void RevertBack()
    {
        collisionOn2DPlane.excludeLayers = LayerMask.GetMask("Nothing");
        collisionOn2DPlane.excludeLayers = LayerMask.GetMask("Draggable");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
        print("Revert Back Called");
    }
    //JACOB END
}
