using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum SettingType
{
    CameraSensitivity,
}

[CreateAssetMenu(fileName = "PlayerSettingConfig", menuName = "Config/Player Setting")]
public class PlayerSettingConfig : ScriptableObject
{
    [Title("Camera Setting")]
    public float ViewXSensitivity;
    public float ViewYSensitivity;
    public float CameraSensitivityMultiplier;
    public float MinViewX;
    public float MaxViewX;
    public float CamStandHeight;
    public float CamCrouchHeight;
    public float CamProneHeight;
    public float playerStanceSmoothing;
}   
