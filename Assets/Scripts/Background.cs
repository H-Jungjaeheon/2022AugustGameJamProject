using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] GameObject target = null;

    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (LogicManager.Inst != null)
    //    {
    //        LogicManager.Inst.playerObj.GetComponent<PlayerMovement>().Die();
    //    }
    //}
}
