using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController _characterController;
    [HideInInspector] public Animator _animator;
    protected CharacterControlCombat _controlCombat;

    protected bool isPaused;
    // [HideInInspector] public CharacterControlAnimator _controlAnimator;
    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _controlCombat = GetComponent<CharacterControlCombat>();
    }

    protected virtual void Update()
    {

    }
    // protected virtual void LateUpdate()
    // {
    //
    // }
    protected virtual void FixedUpdate()
    {

    }
}
