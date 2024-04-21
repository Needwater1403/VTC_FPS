using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem VFX_hit;
    [SerializeField] private Rigidbody rb;
    public ConfigBulletSO configBullet;
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * (configBullet.speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.EnemyTag)) return;
        if (other.CompareTag(configBullet.targetTag))
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(configBullet.damage);
                Blink(other);
            }        
        }
        Instantiate(VFX_hit, transform.position, Quaternion.identity);
        StartCoroutine(DetroyBullet());
    }
    private void Blink(Component other)
    {
        var controlAnimator = other.GetComponent<CharacterControlAnimator>();
        StartCoroutine(controlAnimator.MaterialBlink());
    }

    IEnumerator DetroyBullet()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
