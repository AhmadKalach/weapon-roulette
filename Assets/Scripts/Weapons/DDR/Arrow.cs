using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowType;

    RythmGameScript DDRScript;
    float endTime;

    // Start is called before the first frame update
    void Start()
    {
        endTime = Time.time + 0.7f;
        DDRScript = GameObject.FindGameObjectWithTag("DDR").GetComponent<RythmGameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(endTime - Time.time) < 0.15f)
        {
            if (arrowType == 0 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DDRScript.expandLightning();
            }
            else if (arrowType == 1 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                DDRScript.expandLightning();
            }
            else if (arrowType == 2 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                DDRScript.expandLightning();
            }
            else if (arrowType == 3 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                DDRScript.expandLightning();
            }
        }
        else if (Time.time > endTime + 0.15f)
        {
            Destroy(this.gameObject);
        }
    }
}
