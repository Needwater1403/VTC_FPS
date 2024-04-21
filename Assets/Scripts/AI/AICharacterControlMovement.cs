using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AICharacterControlMovement : CharacterControlMovement
{
    [HideInInspector] public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private PlayerManager _player;
    public float aggroRange = 17;
    [HideInInspector] public bool canRotate;
    protected override void Awake()
    {
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _player = GameManager.Instance.Player;
    }
    
    public void HandleAIPursueMovement(Action<AIState> _test, List<AIState> _list)
    {
        if(Vector3.Distance(_player.transform.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
        else
        {
            _test?.Invoke(_list[2]);
        }
    }
    public void HandleAIAggroRange(Action<AIState> _test, List<AIState> _list)
    {
        if (!_player.transform || _player.IsDead) return;
        if(Vector3.Distance(_player.transform.position, transform.position) <= aggroRange)
        {
            _test?.Invoke(_list[1]);
        }
    }
    public void HandleAIAttackRange(Action<AIState> _test, List<AIState> _list)
    {
        
        if (!_player.transform || _player.IsDead)
        {
            _test?.Invoke(_list[0]);
            return;
        }
        if(Vector3.Distance(_player.transform.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            canRotate = false;
            _test?.Invoke(_list[1]);
        }
    }
    
    public void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (canRotate)
        {
            LookAtTarget();
        }
        
    }

    public void LookAtTarget()
    {
        transform.LookAt(_player.transform, Vector3.up);
    }
    
}
