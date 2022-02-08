using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = (mousePos - (Vector2)transform.position);
        if (diff.magnitude > 0.1f)
        {
            rb.velocity = (mousePos - (Vector2)transform.position).normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
