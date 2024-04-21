using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class AICharacterControlAnimator : CharacterControlAnimator
{
    public float moveAmount;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool isDead;
    [Title("VFX")]
    [SerializeField] private VisualEffect VFX_footStep;
    [SerializeField] private VisualEffect VFX_attack;
    [SerializeField] private ParticleSystem VFX_BeingHit;
    public void HandleAllAnimation()
    {
        AIUpdateAnimation(0,moveAmount, isDead, isAttacking);
    }

    private void FootStepBurstParticle()
    {
        VFX_footStep.Play();
    }

    private void AttackParticle()
    {
        VFX_attack.Play();
    }

    public void BeingHitVFX(Vector3 _playerPos)
    {
        var dir = transform.position - _playerPos;
        dir.Normalize();
        dir.y = 0;
        VFX_BeingHit.transform.rotation = Quaternion.LookRotation(dir);
        VFX_BeingHit.Play();
    }
    
    protected override void Awake()
    {
        base.Awake();
        _Renderer.enabled = false;
        canMove = true;
        isAttacking = false;
        isDead = false;
    }
}
