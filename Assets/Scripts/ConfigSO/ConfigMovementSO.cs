using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigMovementSO", menuName = "Config/Config Movement")]
public class ConfigMovementSO : ScriptableObject
{
    [Title("Moving")]
    [Header("Walk")]
    public float walkFowardSpeed;
    public float walkBackwardSpeed;
    public float walkStrafeSpeed;
    [Header("Sprint")]
    public float sprintSpeed;
    [Header("Crouch")]
    public float crouchFowardSpeed;
    public float crouchBackwardSpeed;
    public float crouchStrafeSpeed;
    [Title("Jump")]
    public float jumpForce;
    public float jumpSpeed;
    public float jumpCooldown;
    
}
