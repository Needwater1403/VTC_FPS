using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Only store the input value from the player, and can be accessed by other classes, do not handle any logic
/// </summary>
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;
    
    public Vector2 MovementInputValue => movementInputValue;
    public Vector2 LookInputValue => lookInputValue;
    public bool JumpInputValue => jumpInputValue;
    public bool SprintInputValue => sprintInputValue;
    public bool AimInputValue => aimInputValue;
    public bool ShootInputValue => shootInputValue;
    public float MoveAmount => moveAmount;
    
    private Vector2 movementInputValue;
    private  Vector2 lookInputValue;
    private bool jumpInputValue;
    private bool sprintInputValue;
    private bool aimInputValue;
    private bool shootInputValue;
    private float moveAmount;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        movementInputValue  = _context.ReadValue<Vector2>();
        moveAmount = Mathf.Clamp01(Mathf.Abs(movementInputValue.x) + Mathf.Abs((movementInputValue.y)));
        
        //CHANGE BETWEEN WALK AND RUN ANIMATION
        switch (moveAmount)
        {
            case <= 0.5f and > 0:
                moveAmount = 0.5f;
                break;
            case <= 1 and > 0.5f:
                moveAmount = 1;
                break;
        }
    }

    public void OnLook(InputAction.CallbackContext _context)
    {
        lookInputValue  = _context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Performed:
                jumpInputValue = true;
                break;
            case InputActionPhase.Canceled:
                jumpInputValue = false;
                break;
        }
    }
    public void OnSprint(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Performed:
                sprintInputValue = true;
                break;
            case InputActionPhase.Canceled:
                sprintInputValue = false;
                break;
        }
    }
    public void OnAim(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Performed:
                aimInputValue = true;
                break;
            case InputActionPhase.Canceled:
                aimInputValue = false;
                break;
        }
    }
    public void OnShoot(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Performed:
                shootInputValue = true;
                break;
            case InputActionPhase.Canceled:
                shootInputValue = false;
                break;
        }
    }
}