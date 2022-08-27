using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public string enemy;
    public int nextMove;
    public float moveSpeed;
    public float power = 10;
    public float maxShotDelay;
    //public float curShotDelay;
    public Sprite[] sprites;
    //public GameObject  bulletObject;
    private GameObject player;

    Transform playerPos;
    Vector3 dir;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //if(GetComponent<Rigidbody2D>())
        //    this.gameObject.AddComponent<Rigidbody2D>();

        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
    }

    private void Start()
    {
        player = LogicManager.Inst.playerObj;
        StartCoroutine(Shot());
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //Move();
        //Shot();
    }
    //犁蓖侥
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 5);
    }

    IEnumerator Shot()
    {
        while(true)
        {
            yield return new WaitForSeconds(maxShotDelay);
            //GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            GameObject bullet = BulletPoolMgr.Inst.bulletPool.GetObject();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();

            if (LogicManager.Inst != null)
                playerPos = LogicManager.Inst.playerObj.transform;
            else
                playerPos = GameObject.Find("Player").GetComponent<Transform>();

            dir = player.transform.position - transform.position;

            rig.AddForce(dir * power, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        Move();
    }
    //void Shot()
    //{
    //    if (curShotDelay < maxShotDelay)
    //        return;

    //    //GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
    //    GameObject bullet = BulletPoolMgr.Inst.bulletPool.GetObject();
    //    bullet.transform.position = transform.position;
    //    bullet.transform.rotation = transform.rotation;

    //    Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();

    //    if(LogicManager.Inst != null)
    //        playerPos = LogicManager.Inst.playerObj.transform;
    //    else
    //        playerPos = GameObject.Find("Player").GetComponent<Transform>();

    //    dir = player.transform.position - transform.position;

    //    rig.AddForce(dir * power, ForceMode2D.Impulse);
    //    curShotDelay = 0;
    //}
    void Move()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f)//哭率 场
            pos.x = 0f;
        if (pos.x > 1f) //坷弗率 场
            pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y * moveSpeed);

    }
}
