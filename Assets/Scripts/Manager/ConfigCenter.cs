using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCenter : MonoBehaviour
{
    public static ConfigCenter Instance;
    [SerializeField] private PlayerSettingConfig playerSetting;
    [SerializeField] private ConfigMovementSO configMovement;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public PlayerSettingConfig GetPlayerSetting()
    {
        return playerSetting;
    }
    public ConfigMovementSO GetConfigMovement()
    {
        return configMovement;
    }
}
