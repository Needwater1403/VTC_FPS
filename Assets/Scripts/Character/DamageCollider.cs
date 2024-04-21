using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private CharacterControlCombat _controlCombat;
    [SerializeField] private Transform parent;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_controlCombat.health.configCombat.targetTag) && !_controlCombat.targetList.Contains(other))
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(_controlCombat.health.configCombat.normalATK);
                Debug.Log($"<color=red>{health.gameObject.tag}</color> Current HP: {health.CurrentHp}");
            }        
            _controlCombat.targetList.Add(other);
        }
    }
    
    
}
