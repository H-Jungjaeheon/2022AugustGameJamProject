using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKillPooled : MonoBehaviour
{
    public float Delay = 2.0f;
    public bool parent = false;
    private PooledObject pooledObject;
    private float accTime;

    private void OnEnable()
    {
        accTime = 0.0f;
    }

    private void Start()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    private void Update()
    {
        accTime += Time.deltaTime;
        if (accTime >= Delay)
            pooledObject.Pool.ReturnObject(gameObject, parent);
    }
}
