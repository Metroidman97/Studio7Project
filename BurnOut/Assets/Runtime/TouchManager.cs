using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchPressAction;
    private InputAction touchPositionAction;

    private Camera mainCamera;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");

        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        touchPressAction.Enable();
        touchPositionAction.Enable();
    }

    private void OnDisable()
    {
        touchPressAction.Disable();
        touchPositionAction.Disable();
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {        
        Ray ray = mainCamera.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("TapTarget"))
        {
            hit.collider.gameObject.TryGetComponent(out SigilPoint point);
            if (point != null)
            {
                point.PointTapped();
            }
        }
    }
}
