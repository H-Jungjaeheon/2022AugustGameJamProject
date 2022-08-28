using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Vector3[] objScale;

    [SerializeField]
    private bool isRandResourceChange;

    SpriteRenderer SR;

    void Awake()
    {
        RandResource();
    }

    private void RandResource()
    {
        if (isRandResourceChange == false) return;

        SR = GetComponent<SpriteRenderer>();
        int resourceIndex = Random.Range(0, 3);
        SR.sprite = sprites[resourceIndex];
        transform.localScale = objScale[resourceIndex];
        //switch (resourceIndex)
        //{
        //    case 0:
        //        SR.sprite = sprites[0];
        //        break;
        //    case 1:

        //        break;
        //    case 2:

        //        break;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SupplyBoxDestryZone"))
        {
            SupplyBoxSpawner.Inst.NowSupplyBoxCount--;
            Destroy(gameObject);
        }
    }
}
