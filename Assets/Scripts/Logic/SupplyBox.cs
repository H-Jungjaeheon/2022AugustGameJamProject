using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("SupplyBoxDestryZone"))
        //{
        //    SupplyBoxSpawner.Inst.NowSupplyBoxCount--;
        //    if(GetComponent<PooledObject>())
        //        GetComponent<PooledObject>().Pool.ReturnObject(gameObject);
        //    else
        //        Destroy(gameObject);
        //}
        if (collision.gameObject.CompareTag("SupplyBoxDestryZone"))
        {
            SupplyBoxSpawner.Inst.NowSupplyBoxCount--;
            gameObject.SetActive(false);
        }
    }
}
