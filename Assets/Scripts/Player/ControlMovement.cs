using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlMovement : CharacterControlMovement
{
    private PlayerManager _playerManager;
    
    private Transform tf;
    public Transform TF => tf;

    private Vector2 moveValue;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    private float velocityY;
    private bool canMove;
    private bool isRoll;
    private bool isAttack;
    
    public ConfigMovementSO configMovement;
    protected override void Awake()
    {
        tf = transform;
        _playerManager = GetComponent<PlayerManager>();
    }

    private void GetMovementInputValue()
    {
        moveValue = ReceiveInput.Instance.MovementInputValue;
    }
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {
        //Grounded movement handle
        HandleGroundMovement();
        //Rotation handle
        HandleRotation();
    }

    
    private void HandleGroundMovement()
    {
        GetMovementInputValue();
        moveDir.Set(moveValue.x,0,moveValue.y);
        moveDir.Normalize();
        moveDir = Quaternion.Euler(0, -45f, 0) * moveDir;
        moveDir.y = 0;
        
        //-----------------MOVE-----------------
        if (ReceiveInput.Instance.MoveAmount <= 0.5f)
        {
            moveDir *= configMovement.walkSpeed;
        }
        else if (ReceiveInput.Instance.MoveAmount <= 1)
        {
            moveDir *= configMovement.runSpeed;
        }
        
        //------------HANDLE GRAVITY------------
        velocityY = !_playerManager._characterController.isGrounded ? configMovement.gravity : configMovement.gravity * 0.3f;
        moveDir += velocityY * Vector3.up;
        _playerManager._characterController.Move(moveDir * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        #region 3rdPS
        rotationDir = Vector3.zero;
        rotationDir.Set(moveValue.x,0,moveValue.y);
        rotationDir.Normalize();
        rotationDir = Quaternion.Euler(0, -45f, 0) * rotationDir;
        rotationDir.y = 0;
        if (rotationDir == Vector3.zero)
        {
            rotationDir = transform.forward;
        }
        var newDir = Quaternion.LookRotation(rotationDir);
        var camDir = Quaternion.Slerp(transform.rotation, newDir, configMovement.rotationSpeed * Time.deltaTime);
        transform.rotation = camDir;
        #endregion
        
        //rotationDir
    }
    
}
