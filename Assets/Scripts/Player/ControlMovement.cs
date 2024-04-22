using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlMovement : CharacterControlMovement
{
    //private PlayerManager _playerManager;
    
    private Transform tf;
    public Transform TF => tf;
    
    private Vector3 moveDir;
    private Vector3 rotationDir;
    private Vector3 camRotation;
    private float velocityY;
    private bool canMove;
    private bool isRoll;
    private bool isAttack;
    private float targetMoveDirY;
    private bool expectGrounded;
    private bool lockInJump;
    private float jumpSpeed;
    private float lastJumpTime;
    private float camHeight;
    private float camHeightVelocity;
    
    public ConfigMovementSO configMovement;
    public PlayerSettingConfig playerConfig;
    protected override void Awake()
    {
        base.Awake();
        tf = transform;
    }

    private void Start()
    {
        camHeight = GameManager.Instance.Player.camHolder.position.y;
        camRotation = GameManager.Instance.Player.camHolder.localRotation.eulerAngles;
        rotationDir = GameManager.Instance.Player.transform.localRotation.eulerAngles;
    }
    
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {
        HandleGroundMovement();
        HandleCameraView();
        HandleJump();
        HandleCameraHeight();
    }

    
    private void HandleGroundMovement()
    {
        //------------HANDLE MOVEMENT------------
        switch (GameManager.Instance.Player.playerStance)
        {
            case Constants.PlayerStance.Standing:
            {
                var transform1 = GameManager.Instance.Player.camHolder.transform;
                moveDir = transform1.forward * (ReceiveInput.Instance.MovementInputValue.y * (ReceiveInput.Instance.MovementInputValue.y > 0? 
                    (ReceiveInput.Instance.SprintInputValue? configMovement.sprintSpeed : configMovement.walkFowardSpeed)  : configMovement.walkBackwardSpeed) * Time.deltaTime);
                moveDir += transform1.right *  (ReceiveInput.Instance.MovementInputValue.x * configMovement.walkStrafeSpeed * Time.deltaTime);
                moveDir.y = 0;
                jumpSpeed = configMovement.jumpSpeed;
                break;
            }
            case Constants.PlayerStance.Crouching:
            {
                var transform1 = GameManager.Instance.Player.camHolder.transform;
                moveDir = transform1.forward * (ReceiveInput.Instance.MovementInputValue.y * (ReceiveInput.Instance.MovementInputValue.y > 0? 
                    configMovement.crouchFowardSpeed : configMovement.crouchBackwardSpeed) * Time.deltaTime);
                moveDir += transform1.right *  (ReceiveInput.Instance.MovementInputValue.x * configMovement.crouchStrafeSpeed * Time.deltaTime);
                moveDir.y = 0;
                jumpSpeed = configMovement.jumpSpeed * 3/5;
                break;
            }
        }
        
        //------------HANDLE GRAVITY------------
        if (!GameManager.Instance.Player._characterController.isGrounded)
        {
            if (transform.position.y > moveDir.y)
            {
                targetMoveDirY += Physics.gravity.y * Time.deltaTime;
                if(!expectGrounded) expectGrounded = true;
            }
        }
        else
        {
            if (expectGrounded)
            {
                moveDir = Vector3.zero;
                expectGrounded = false;
                lockInJump = false;
            }
        }
        moveDir.y = Mathf.Lerp(moveDir.y, targetMoveDirY, jumpSpeed * Time.deltaTime);
        GameManager.Instance.Player._characterController.Move(moveDir);
    }
    private void HandleCameraView()
    {
        camRotation.x -= playerConfig.ViewYSensitivity * ReceiveInput.Instance.LookInputValue.y * playerConfig.CameraSensitivityMultiplier * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, playerConfig.MinViewX, playerConfig.MaxViewX);
        rotationDir.y += playerConfig.ViewXSensitivity * ReceiveInput.Instance.LookInputValue.x * playerConfig.CameraSensitivityMultiplier * Time.deltaTime;
        GameManager.Instance.Player.camHolder.localRotation = Quaternion.Euler(camRotation);
        GameManager.Instance.Player.transform.localRotation = Quaternion.Euler(rotationDir);
    }

    private void HandleJump()
    {
        if(Time.realtimeSinceStartup - lastJumpTime < configMovement.jumpCooldown) return;
        var _jump = ReceiveInput.Instance.JumpInputValue;
        if (!_jump) return;
        OnJump();
    }
    public void OnJump()
    {
        if (!GameManager.Instance.Player._characterController.isGrounded) return;
        lastJumpTime = Time.realtimeSinceStartup;
        targetMoveDirY = configMovement.jumpForce;
        lockInJump = true;
    }

    private void HandleCameraHeight()
    {
        var targetHeight = ReceiveInput.Instance.CrouchInputValue ? playerConfig.CamCrouchHeight : playerConfig.CamStandHeight;
        var localPosition = GameManager.Instance.Player.camHolder.localPosition;
        camHeight = Mathf.SmoothDamp(localPosition.y, targetHeight, ref camHeightVelocity,
                                    playerConfig.playerStanceSmoothing);
        localPosition = new Vector3(localPosition.x, camHeight, localPosition.z);
        GameManager.Instance.Player.camHolder.localPosition = localPosition;
    }
}
