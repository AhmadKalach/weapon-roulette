using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public float speed = 2;
    public float distanceToStop = 0.1f;
    public Transform toFollow;
    public Transform sprite;
    public bool isEnabled = true;
    public bool hasIdle = false;

    Rigidbody2D rb;
    Animator animator;

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
        if (toFollow == null)
        {
            toFollow = GameObject.FindGameObjectWithTag("Player").transform;
        }


        if (toFollow != null)
        {
            Vector2 followPos = toFollow.transform.position;
            Vector2 diff = (followPos - (Vector2)transform.position);
            if (diff.magnitude > distanceToStop)
            {
                animator.SetBool("isRunning", true);
                rb.velocity = (followPos - (Vector2)transform.position).normalized * speed;
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
}
