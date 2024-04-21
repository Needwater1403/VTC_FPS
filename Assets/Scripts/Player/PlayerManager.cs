using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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
    
    private float coinNum = 0;
    public float CoinNum => coinNum;
    private bool isDead;
    public bool IsDead => isDead;

    
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(transform.parent);
    }

    protected override void Update()
    {
        if(isDead) return;
        base.Update();
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
}
