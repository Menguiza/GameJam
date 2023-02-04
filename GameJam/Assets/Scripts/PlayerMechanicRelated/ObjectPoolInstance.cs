using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolInstance : MonoBehaviour
{
    private Transform _transform;
    private byte poolIndex;
    private Gun gun;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void SetPool(byte index, Gun storage)
    {
        poolIndex = index;
        gun = storage;
    }

    public void Return()
    {
        gun.Return(this, poolIndex);
    }

    internal void Prepare()
    {
        gameObject.SetActive(true);
    }

    internal void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            Return();
        }
    }
}
