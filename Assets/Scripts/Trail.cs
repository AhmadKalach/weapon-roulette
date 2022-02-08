using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trail : MonoBehaviour
{
    public float shakeDuration;
    public float shakeStrength;
    public AudioSource hitSound;
    public float timeToActivate;

    float activeTime;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        activeTime = Time.time + timeToActivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > activeTime)
        {
            isActive = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isActive)
        {
            Camera.main.DOShakePosition(shakeDuration, shakeStrength);
            collision.gameObject.GetComponent<Player>().Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isActive)
        {
            Camera.main.DOShakePosition(shakeDuration, shakeStrength);
            Destroy(collision.gameObject);
        }
    }
}
