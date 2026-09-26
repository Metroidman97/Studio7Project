using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchDrag;

    [SerializeField]
    private float dragPhysicsSpeed = 10f;
    [SerializeField]
    private float touchDragSpeed = 0.1f;


    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
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

    private void Update()
    {
        
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Grabbed");
        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        //Debug.Log(ray);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null && (hit.collider.gameObject.CompareTag("Draggable") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Draggable")))
        {
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        while (touchDrag.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * dragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, touchDragSpeed);
                yield return null;
            }
        }
    }
}
