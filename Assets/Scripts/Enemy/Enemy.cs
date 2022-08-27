using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextMove;
    public float moveSpeed;
    public float power = 100;
    public float maxShotDelay;
    public float curShotDelay;
    public Sprite[] sprites;
    public GameObject  bulletObject;
    public GameObject player;
    Vector3 dir;
    //public ObjectPool bulletPool = null;
    Transform playerPos;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 2);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Shot();
        Reload();
    }
    //犁蓖侥
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove == 0)
            return;
        Invoke("Think", 2);
    }
    void Shot()
    {
        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = BulletPoolMgr.Inst.bulletPool.GetObject();
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        if (LogicManager.Inst != null)
        {
            dir = LogicManager.Inst.playerObj.transform.position - transform.position;
        }
        else
        {
            dir = Vector3.zero - transform.position;
        }
        rig.velocity = Vector2.zero;
        rig.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        curShotDelay = 0;
    }

    void Move()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigid.rotation = angle;
        if (pos.x < 0f)//哭率 场
            pos.x = 0.9f;
        if (pos.x > 1f) //坷弗率 场
            pos.x = 0.9f;
        if (pos.y < 0f) pos.y = 0.1f;
        if (pos.y > 1f) pos.y = 0.9f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        rigid.velocity = new Vector2(nextMove, 0) * moveSpeed;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
