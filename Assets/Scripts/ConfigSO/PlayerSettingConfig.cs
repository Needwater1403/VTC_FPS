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
    public float CameraHorizontalSpeed;
    public float CameraVerticalSpeed;
    public float CameraSensitivityMultiplier;
}   
