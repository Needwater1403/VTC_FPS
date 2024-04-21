using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Attack")]
public class AttackState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        //SWITCH TO PURSUE STATE
        if(!aiCharacterManager._controlAnimator.canMove)
        {
            aiCharacterManager._controlMovement.HandleAIAttackRange(aiCharacterManager.SwitchStateTo,
                aiCharacterManager.stateList);
        }
        // HANDLE ATTACK ANIMATION
        if(!aiCharacterManager._controlAnimator.isAttacking && aiCharacterManager._controlAnimator.canMove)
        {
            if (aiCharacterManager._controlMovement._navMeshAgent.stoppingDistance < 3)
            {
                aiCharacterManager._controlMovement.LookAtTarget();
            }
            else aiCharacterManager._controlMovement.canRotate = true;
            aiCharacterManager._controlAnimator.isAttacking = true;
        }
        return base.Tick(aiCharacterManager);
    }
}
