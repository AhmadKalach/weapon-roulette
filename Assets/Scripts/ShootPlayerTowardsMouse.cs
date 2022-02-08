using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootPlayerTowardsMouse : BaseWeapon
{
    public GameObject player;
    public GameObject arrowParent;
    public float shootAfterInSeconds;
    public float shootSpeed;
    public float timeout;
    public AudioSource hitEnemy;
    public AudioSource hitWall;

    float timeToShoot;
    float timeoutTime;
    bool playerShot;

    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = Time.time + shootAfterInSeconds;
        timeoutTime = Time.time + timeout;
        playerShot = false;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        timeToShoot = Time.time + shootAfterInSeconds;
        timeoutTime = Time.time + timeout;
        player.GetComponent<Movement>().isEnabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Player>().immune = true;
        playerShot = false;
        arrowParent.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToShoot && !playerShot)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diff = (mousePos - (Vector2)transform.position);
            player.GetComponent<Rigidbody2D>().velocity = diff.normalized * shootSpeed;
            playerShot = true;
            arrowParent.SetActive(false);
        }

        if (Time.time > timeoutTime)
        {
            DisableWeapon();
        }
    }

    void DisableWeapon()
    {
        player.GetComponent<Movement>().isEnabled = true;
        player.GetComponent<Player>().immune = false;
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
            hitEnemy.Stop();
            hitEnemy.Play();
        }
        else if (collision.gameObject.tag.Equals("Wall"))
        {
            hitWall.Stop();
            hitWall.Play();
            Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
            hitEnemy.Stop();
            hitEnemy.Play();
        }
        else if (collision.gameObject.tag.Equals("Wall"))
        {
            hitWall.Stop();
            hitWall.Play();
            Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        }
    }
}
