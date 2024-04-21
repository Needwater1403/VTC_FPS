using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigCombatSO", menuName = "Config/Config Bullet")]
public class ConfigBulletSO : ScriptableObject
{
    public string targetTag;
    public float speed;
    public float damage;
}
