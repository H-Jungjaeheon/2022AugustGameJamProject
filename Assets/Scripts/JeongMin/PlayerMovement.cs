using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Hang,
    Rush,
    Jump
}

public class PlayerMovement : MonoBehaviour
{
    PlayerState currentState;

    [SerializeField] float playerSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float attackDistance;

    [SerializeField] Sprite[] P_Sprite = new Sprite[3];

    private float moveInput;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Hook playerHook;
    private SpriteRenderer spriteRenderer;

    //반사
    private bool isReflex;
    private bool isReachEnemy;
    private void Start()
    {
        currentState = PlayerState.Jump;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerHook = GetComponent<Hook>();
        boxCollider = GetComponent<BoxCollider2D>();

        rb.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(moveInput);
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (playerHook.isHang) currentState = PlayerState.Hang;
        SpriteChange();
        Rush();
        Jump();        
    }

    void SpriteChange()
    {
        if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        spriteRenderer.sprite = P_Sprite[(int)currentState];
    }

    void Move(float dir)
    {
        if (playerHook.isHang)
        {
            rb.AddForce(new Vector2(dir * (playerSpeed + 8), 0));
        }
        else
        {
            rb.velocity = new Vector2(dir * playerSpeed, rb.velocity.y);
        }
    }

    void Rush()
    {
        if (!playerHook.isHang) return;

        if (Input.GetMouseButton(0))
        {
            Vector2 rushPoint = playerHook.hook.position + new Vector3(1,1,0) * 3f;
            playerHook.PutHook();
            StartCoroutine(Rushing(rushPoint));
        }
    }

    IEnumerator Rushing(Vector3 point)
    {
        currentState = PlayerState.Rush;
        SoundPlayer.PlaySoundFx("Dash_Sound");
        boxCollider.enabled = false;
        isReflex = true;
        while (true)
        {
            if (Vector2.Distance(transform.position, point) <= 1.1f) break;

            transform.position = Vector2.MoveTowards(transform.position, point, rushSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        isReflex = false;
        boxCollider.enabled = true;
    }

    void Jump()
    {
        if (!playerHook.isHang) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = PlayerState.Jump;
            SoundPlayer.PlaySoundFx("Jump_Sound");
            playerHook.PutHook();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void CatchEnemy(Vector2 EnemyPos)
    {
        playerHook.PutHook();
        StartCoroutine(RushToEnemy(EnemyPos));
    }

    IEnumerator RushToEnemy(Vector2 Point)
    {
        currentState = PlayerState.Rush;
        while (true)
        {
            if (isReachEnemy) break;

            transform.position = Vector2.MoveTowards(transform.position, Point, 35 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator KillEnemy(GameObject enemy)
    {
        SoundPlayer.PlaySoundFx("Attack_Sound");

        LogicManager.Inst.EnemyHit(0.5f);
        //공격 애니메이션
        Destroy(enemy, 0.1f);
        yield return new WaitForSeconds(0.2f);

        float curTime1 = 0;
        float limTime1 = Time.time + 0.33f;
        while (curTime1 <= limTime1)
        {
            curTime1 = Time.time;

            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = new Vector2(-jumpForce, jumpForce);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = new Vector2(jumpForce, jumpForce);
                break;
            }
            yield return null;
        }
        currentState = PlayerState.Jump;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce + 2);

        float curTime2 = 0;
        float limTime2 = Time.time + 1f;
        while (curTime2 <= limTime2)
        {
            curTime2 = Time.time;
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                Vector3 point = transform.position + mouseDir.normalized * 10;

                StartCoroutine(Rushing(point));
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isReachEnemy = true;
            StartCoroutine(KillEnemy(collision.gameObject));
        }
        ////else if (collision.gameObject.CompareTag("Bullet"))
        //else if (collision.gameObject.GetComponent<Bullet>())
        //{
        //    if (isReflex)
        //    {
        //        Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
        //        rig.velocity = Vector3.zero;

        //        var dir = collision.gameObject.transform.position - transform.position;
        //        rig.AddForce(dir * (rig.velocity.magnitude * 3), ForceMode2D.Impulse);
        //    }

        //    //반사가 아닐 시 게임오버 처리
        //    Die();
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            if (isReflex)
            {
                Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
                rig.velocity = Vector3.zero;

                var dir = collision.gameObject.transform.position - transform.position;

                SoundPlayer.PlaySoundFx("Reflect_Sound");
                rig.AddForce(dir * (rig.velocity.magnitude * 3), ForceMode2D.Impulse);
            }

            Die();
        }
    }

    public void Die()
    {
        LogicManager.Inst.GameOver();
    }
}
