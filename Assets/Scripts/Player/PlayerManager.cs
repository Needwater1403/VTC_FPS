using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Title("References")]
    public ControlMovement _controlMovement;
    public ControlAnimator _controlAnimator;
    [SerializeField] private Health health;
    public Health Health => health;
    
    [Title("Camera")]
    public Transform camHolder;
    
    [Title("Player")]
    public Constants.PlayerStance playerStance;
    [SerializeField] private CapsuleCollider standCollider;
    [SerializeField] private CapsuleCollider crouchCollider;
    private float capsuleHeightVelocity;
    private Vector3 capsuleCenterVelocity;
    private float coinNum = 0;
    public float CoinNum => coinNum;
    private bool isDead;
    public bool IsDead => isDead;

    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        if(isDead) return;
        base.Update();
        playerStance = ReceiveInput.Instance.CrouchInputValue ? Constants.PlayerStance.Crouching : Constants.PlayerStance.Standing;
        ChangeCollider();
        if (_controlCombat.health.CurrentHp <= 0)
        {
            isDead = true;
            //_controlAnimator.isDead = true;
        }
        // if (_controlAnimator)
        // {
        //     _controlAnimator.HandleAllAnimation(isPaused);
        // }
        if (_controlMovement)
        {
            _controlMovement.HandleAllMovement();
        }
    }
    public float GetMaxHP()
    {
        return health.configCombat.maxHP;
    }
    public float GetCurrentHP()
    {
        return health.CurrentHp;
    }
    public void AddCoin(int amount)
    {
        coinNum += amount;
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
    }

    private void ChangeCollider()
    {
        switch (GameManager.Instance.Player.playerStance)
        {
            case Constants.PlayerStance.Standing:
            {
                _characterController.height = Mathf.SmoothDamp(_characterController.height, standCollider.height,
                    ref capsuleHeightVelocity, _controlMovement.playerConfig.playerStanceSmoothing);
                _characterController.center = Vector3.SmoothDamp(_characterController.center, standCollider.center,
                    ref capsuleCenterVelocity, _controlMovement.playerConfig.playerStanceSmoothing);
                break;
            }
            case Constants.PlayerStance.Crouching:
            {
                _characterController.height = Mathf.SmoothDamp(_characterController.height, crouchCollider.height,
                    ref capsuleHeightVelocity, _controlMovement.playerConfig.playerStanceSmoothing);
                _characterController.center = Vector3.SmoothDamp(_characterController.center, crouchCollider.center,
                    ref capsuleCenterVelocity, _controlMovement.playerConfig.playerStanceSmoothing);
                break;
            }
        }
        
    }
}
