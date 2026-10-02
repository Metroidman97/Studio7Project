using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    // Input action assets
    private PlayerInput playerInput;
    private InputAction touchDrag;

    // Values for object moving physics (make the object trail behind when dragging)
    [SerializeField]
    private float dragPhysicsSpeed = 10f;
    [SerializeField]
    private float touchDragSpeed = 0.1f;


    private Camera mainCamera;                                                  // Main camera
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();   // Wait for seconds precall, so as to not create it constantly
    private Vector3 velocity = Vector3.zero;                                    // Initilize velocity vector as zero

    private void Awake()
    {
        // Get the camera and input actions
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        touchDrag = playerInput.actions.FindAction("TouchPress");
    }

    private void OnEnable()
    {
        touchDrag.Enable();
        touchDrag.performed += MousePressed;
        
    }

    private void OnDisable()
    {
        touchDrag.performed -= MousePressed;
        touchDrag.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());   // Create a ray where the player touches the screen
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);                                           // Detect what the ray hits

        // If the ray hits something on the draggable layer, run the code
        if (hit.collider != null && (hit.collider.gameObject.CompareTag("Draggable") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Draggable")))
        {
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);  // Get the initial distance between the selected object and the camera
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);                                                     // Get the objects rigidbody, if it has one

        // While the player is touching the screen
        while (touchDrag.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());   // Create another ray where the player touches the screen
            if (rb != null)     // If the object has a rigidbody
            {
                // Move the object by changing its velocity
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * dragPhysicsSpeed;
                yield return waitForFixedUpdate;    // Update in fixed update due to physics
            }
            else                // If the object doesn't have a rigidbody
            {
                // Update the object's position based on where the touch is happening
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, touchDragSpeed);
                yield return null;
            }
        }
    }
}
