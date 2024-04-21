using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Idle")]
public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        aiCharacterManager._controlAnimator.moveAmount = 0;
        aiCharacterManager._controlMovement.HandleAIAggroRange(aiCharacterManager.SwitchStateTo, 
            aiCharacterManager.stateList);
        return base.Tick(aiCharacterManager);
    }
}
