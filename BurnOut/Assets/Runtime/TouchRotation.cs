using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchRotation : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchRotate;

    [SerializeField]
    private float touchRotateSpeed = 10f;
    [SerializeField]
    private float angleOffset = -90f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        touchRotate = playerInput.actions.FindAction("TouchPress");
    }

    private void OnEnable()
    {
        touchRotate.Enable();
        touchRotate.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchRotate.performed -= TouchPressed;
        touchRotate.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null &&  hit.collider.gameObject.layer == LayerMask.NameToLayer("Rotatable"))
        {
            StartCoroutine(RotateUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator RotateUpdate(GameObject clickedObject)
    {
        while (touchRotate.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
            Vector3 worldTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3(Touchscreen.current.primaryTouch.position.ReadValue().x, Touchscreen.current.primaryTouch.position.ReadValue().y, Mathf.Abs(mainCamera.transform.position.z)));
            worldTouchPosition.z = 0f;

            Vector2 direction = (worldTouchPosition - clickedObject.transform.position).normalized;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            clickedObject.transform.rotation = Quaternion.Slerp(clickedObject.transform.rotation, targetRotation, touchRotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
