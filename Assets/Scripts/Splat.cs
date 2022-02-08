using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    public float lifetime;
    public GameObject bloodSpot;

    float endTime;

    // Start is called before the first frame update
    void Start()
    {
        endTime = Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > endTime)
        {
            Instantiate(bloodSpot, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(this.gameObject);
        }
    }
}
