using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelfDestruct : BaseWeapon
{
    public float timeToExplode;
    public float explosionRadius;
    public GameObject explosion;
    public SpriteRenderer renderer;

    float explosionTime;
    bool exploded;
    bool flashing;

    public AudioSource beepSource;
    public AudioSource explosionSource;

    // Start is called before the first frame update
    void Start()
    {
        explosionTime = Time.time + timeToExplode;
    }

    private void OnEnable()
    {
        explosionTime = Time.time + timeToExplode;
        beepSource.Play();
        exploded = false;
        explosion.GetComponent<Animator>().SetTrigger("Reset");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > explosionTime && !exploded)
        {
            exploded = true;
            renderer.color = Color.white;
            Explode();
        }
        else if (!exploded)
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
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
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
        }
        explosion.GetComponent<Animator>().SetTrigger("Explode");
        explosionSource.Play();
        Invoke("DisableWeapon", 1.5f);
        beepSource.Stop();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void DisableWeapon()
    {
        explosionSource.Stop();
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }
}
