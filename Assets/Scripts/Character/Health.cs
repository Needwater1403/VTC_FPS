using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public ConfigCombatSO configCombat;
    private float currentHP;
    public float CurrentHp
    {
        get => currentHP;
        set => currentHP = value;
    }

    private void Start()
    {
        currentHP = configCombat.maxHP;
    }

    public void TakeDamage(float _damage)
    {
        currentHP -= _damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
    }

    public void Heal(float _amount)
    {
        currentHP += _amount;
        if (currentHP > configCombat.maxHP)
        {
            currentHP = configCombat.maxHP;
        }
    }
}
