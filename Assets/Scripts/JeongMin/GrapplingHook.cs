using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public PlayerMovement player;
    public DistanceJoint2D joint2D;
    private Hook hook;

    void Start()
    {
        joint2D = GetComponent<DistanceJoint2D>();
        hook = GameObject.Find("Player").GetComponent<Hook>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Supplies"))
        {
            joint2D.enabled = true;
            hook.isHang = true;
        }
        else if (collision.CompareTag("Enemy"))
        {
            player.CatchEnemy(collision.transform.position);
        }
    }
}
