using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float jumpForce;

    private float moveInput;

    private Rigidbody2D rb;
    private Hook playerHook;

    //นÝป็
    private bool isReflex;
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
        while (true)
        {
            if (Vector2.Distance(transform.position, point) <= 1f) break;

            transform.position = Vector2.MoveTowards(transform.position, point, rushSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    void Jump()
    {
        if (!playerHook.isHang) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHook.PutHook();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
