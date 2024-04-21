using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Coin : DropItem
{
    public float rotateSpeed = 2;
    private float angle;
    private void FixedUpdate()
    {
        angle += Time.deltaTime * rotateSpeed;
        transform.rotation = Quaternion.Euler(0, (angle * 180 / math.PI),0);
    }
    protected override void DropAction()
    {
        GameManager.Instance.Player.AddCoin(1);
        //GameManager.Instance.Player._controlAnimator.PickUpCoinVFX();
        UIManager.Instance.txt_coin.SetText(GameManager.Instance.Player.CoinNum.ToString());
        base.DropAction();
    }
}
