using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Spawn")]
public class SpawnState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacterManager)
    {
        if (!aiCharacterManager.isSpawn)
        {
            aiCharacterManager.isSpawn = true;
            aiCharacterManager._controlAnimator.SpawnEffect();
        }
        aiCharacterManager.spawnTimer += Time.deltaTime;
        if (aiCharacterManager.spawnTimer >= aiCharacterManager.spawnDuration)
        {
            aiCharacterManager.spawnTimer = 0;
            aiCharacterManager.SwitchStateTo(aiCharacterManager.stateList[0]);
        }
        return base.Tick(aiCharacterManager);
    }
}
