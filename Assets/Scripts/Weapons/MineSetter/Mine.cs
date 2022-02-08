using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mine : MonoBehaviour
{
    public float shakeDuration;
    public float shakeStrengh;
    public float timeToExplode;
    public float explosionRadius;
    public GameObject explosion;
    public SpriteRenderer renderer;

    float explosionTime;
    bool flashing;
    bool exploded;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        explosionTime = Time.time + timeToExplode;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            if (Time.time + 0.5 > explosionTime)
            {
                renderer.color = Color.red;
            }
            else if (Time.time + 2 > explosionTime)
            {
                if (!flashing)
                {
                    StartCoroutine(Flash(0.1f));
                }
            }
            else
            {
                if (!flashing)
                {
                    StartCoroutine(Flash(0.2f));
                }
            }

            if (Time.time > explosionTime)
            {
                exploded = true;
                Explode();
            }
        }


        if (Time.time > explosionTime + 1)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Flash(float flashTime)
    {
        flashing = true;
        renderer.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        renderer.color = Color.white;
        yield return new WaitForSeconds(flashTime);
        flashing = false;
    }

    void Explode()
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrengh, 10, 90);
        Collider2D[] exploded = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collision in exploded)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Kill();
            }
            if (collision.gameObject.tag.Equals("Damsel"))
            {
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals("Player"))
            {
                Destroy(collision.gameObject);
            }
        }
        explosion.GetComponent<Animator>().SetTrigger("Explode");

        Destroy(renderer);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
