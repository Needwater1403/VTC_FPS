using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlAnimator : MonoBehaviour
{
    private CharacterManager _characterManager;
    private MaterialPropertyBlock _material;
    private SkinnedMeshRenderer _renderer;
    protected SkinnedMeshRenderer _Renderer
    {
        get => _renderer;
        set => _renderer = value;
    }

    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");
    private static readonly int isFall = Animator.StringToHash("isFall");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int isDead = Animator.StringToHash("isDead");
    private static readonly int Roll = Animator.StringToHash("Roll");
    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }
    
    protected void UpdateAnimation(float veloX, float veloY, bool _isDead, bool isAttacking, bool isRoll = false)
    {
        if (_isDead)
        {
            _characterManager._animator.SetTrigger(isDead);
        }
        else
        {
            _characterManager._animator.SetFloat(VelocityX, veloX);
            _characterManager._animator.SetFloat(VelocityZ, veloY);
            _characterManager._animator.SetBool(isFall,!_characterManager._characterController.isGrounded);
        }
    }
    protected void AIUpdateAnimation(float veloX, float veloY, bool _isDead, bool isAttacking = false)
    {
        if (_isDead)
        {    
            _characterManager._animator.SetTrigger(isDead);
        }
        else if (isAttacking)
        {
            _characterManager._animator.SetTrigger(Attack);
        }
        else
        {
            _characterManager._animator.SetFloat(VelocityX, veloX);
            _characterManager._animator.SetFloat(VelocityZ, veloY);
            _characterManager._animator.SetBool(isFall, _characterManager._characterController.isGrounded);
        }
    }
}
