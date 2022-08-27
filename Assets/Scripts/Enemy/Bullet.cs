using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigid;
    private void Update()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector3 dir = LogicManager.Inst.playerObj.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigid.rotation = angle;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<PooledObject>().Pool.ReturnObject(this.gameObject);
            //Destroy(gameObject);
        }
    }
}
