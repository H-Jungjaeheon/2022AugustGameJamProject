using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float attackDistance;

    private float moveInput;

    private Rigidbody2D rb;
    private Hook playerHook;

    //반사
    private bool isReflex;
    private bool isReachEnemy;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHook = GetComponent<Hook>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(moveInput);
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        Rush();
        Jump();
    }

    void Move(float dir)
    {
        if (playerHook.isHang)
        {
            rb.AddForce(new Vector2(dir * playerSpeed + 8, 0));
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
            Vector2 rushPoint = playerHook.hook.position;
            playerHook.PutHook();
            StartCoroutine(Rushing(rushPoint));
        }
    }

    IEnumerator Rushing(Vector3 point)
    {
        SoundPlayer.PlaySoundFx("Dash_Sound");
        isReflex = true;
        while (true)
        {
            if (Vector2.Distance(transform.position, point) <= 1.1f) break;

            transform.position = Vector2.MoveTowards(transform.position, point, rushSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        isReflex = false;
    }

    void Jump()
    {
        if (!playerHook.isHang) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundPlayer.PlaySoundFx("Jump_Sound");
            playerHook.PutHook();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void CatchEnemy(Vector2 EnemyPos)
    {
        StartCoroutine(RushToEnemy(EnemyPos));
    }

    IEnumerator RushToEnemy(Vector2 Point)
    {
        while (true)
        {
            if (isReachEnemy) break;
            
            transform.position = Vector2.MoveTowards(transform.position, Point, 35 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator KillEnemy(GameObject enemy)
    {
        //공격 애니메이션
        SoundPlayer.PlaySoundFx("Attack_Sound");
        Destroy(enemy, 0.2f);

        LogicManager.instance.EnemyHit();
        yield return new WaitForSeconds(0.2f);

        float curTime = 0;
        float limTime = Time.time + 0.33f;
        while (curTime <= limTime)
        {
            curTime = Time.time;

            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = new Vector2(-jumpForce, jumpForce);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = new Vector2(jumpForce, jumpForce);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
    }

}
