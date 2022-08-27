using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    [SerializeField] LineRenderer line;
    public Transform hook;

    private Vector2 mouseDir;

    public float hookMaxLength;
    public float hookSpeed;

    public bool isHang;
    private bool isHookActive;
    private bool isMaxLength;
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        HookShoot();
    }

    void HookShoot()
    {
        if (Input.GetMouseButtonDown(1) && !isHookActive)
        {
            hook.position = transform.position;
            isHookActive = true;
            hook.gameObject.SetActive(true);

            mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }       

        if (isHookActive && !isMaxLength && !isHang)
        {
            hook.Translate(mouseDir.normalized * Time.deltaTime * hookSpeed);
            if (Vector2.Distance(transform.position, hook.position) > hookMaxLength)
            {
                isMaxLength = true;
            }
        }
        else if (isHookActive && isMaxLength && !isHang)
        {
            if(Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isMaxLength = false;
                hook.gameObject.SetActive(false);
            }
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * hookSpeed);
        }
        else if (isHang)
        {
            if (Input.GetMouseButtonUp(1))
            {
                PutHook();
            }
        }
    }

    public void PutHook()
    {       
        isHang = false;
        isHookActive = false;
        isMaxLength = false;
        hook.GetComponent<GrapplingHook>().joint2D.enabled = false;
        hook.gameObject.SetActive(false);
    }
}
