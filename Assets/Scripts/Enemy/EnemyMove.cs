using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.AddComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f)//哭率 场
            pos.x = 0f;
        if (pos.x > 1f) //坷弗率 场
            pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }
    //犁蓖侥
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 5);
    }
}
