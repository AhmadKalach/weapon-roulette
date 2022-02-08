using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        LookAt(mousePosition);
    }

    void LookAt(Vector2 lookAt)
    {
        Vector2 direction = new Vector2(
                lookAt.x - transform.position.x,
                lookAt.y - transform.position.y
            );

        transform.right = direction;
    }
}
