using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class ControlAnimator : CharacterControlAnimator
{
    public float moveAmount;
    private bool isAttacking;
    private bool isRoll;
    [HideInInspector] public bool isDead;
    private void GetMovementInputValue(bool isPaused)
    {
        if (isPaused)
        {
            moveAmount = 0;
            return;
        }
        moveAmount = ReceiveInput.Instance.MoveAmount;
    }
    public void HandleAllAnimation(bool isPaused)
    {
        GetMovementInputValue(isPaused);
        UpdateAnimation(0,moveAmount, isDead, isAttacking, isRoll);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}

