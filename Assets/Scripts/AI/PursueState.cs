using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Pursue")]
public class PursueState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        if (aiCharacterManager._controlAnimator.canMove)
        {
            aiCharacterManager._controlAnimator.moveAmount = 1;
            aiCharacterManager._controlMovement.HandleAIPursueMovement(aiCharacterManager.SwitchStateTo,
                aiCharacterManager.stateList);
        }
        return base.Tick(aiCharacterManager);
    }
}
