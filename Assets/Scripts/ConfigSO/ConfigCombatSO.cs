using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigCombatSO", menuName = "Config/Config Combat")]
public class ConfigCombatSO : ScriptableObject
{
    public string targetTag;
    public float maxHP;
    public float normalATK;
}
