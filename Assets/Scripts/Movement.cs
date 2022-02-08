using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2;
    public Transform sprite;
    public bool isEnabled;

    Rigidbody2D rb;
    Animator animator;
    bool playerDisabledOnce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            MoveTowardsMouse();
        }
    }

    void MoveTowardsMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = (mousePos - (Vector2)transform.position);
        if (diff.magnitude > 0.1f)
        {
            animator.SetBool("isRunning", true);
            rb.velocity = (mousePos - (Vector2)transform.position).normalized * speed;
            if (diff.x > 0)
            {
                sprite.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                sprite.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero;
        }
    }
}
