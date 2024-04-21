using UnityEngine;

[CreateAssetMenu(fileName = "ConfigMovementSO", menuName = "Config/Config Movement")]
public class ConfigMovementSO : ScriptableObject
{
    public float walkFowardSpeed;
    public float walkBackwardSpeed;
    public float walkStrafeSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float jumpSpeed;
    public float jumpCooldown;
}
