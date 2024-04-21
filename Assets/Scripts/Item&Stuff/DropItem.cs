using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    protected Collider _collider;
    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            DropAction();
        }
    }

    protected virtual void DropAction()
    {
        Destroy(gameObject);
    }
}
