using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Snake : BaseWeapon
{
    public GameObject trail;
    public float timeBetweenTrails;
    public float moveSpeed;
    public AudioSource hitSound;
    public float timeout;

    List<GameObject> trails;
    float nextTrailTime;
    float prevSpeed;
    float endTime;

    // Start is called before the first frame update
    void Start()
    {
        trails = new List<GameObject>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        endTime = Time.time + timeout;
        nextTrailTime = Time.time + timeBetweenTrails;
        prevSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().speed;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().immune = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTrailTime)
        {
            GameObject curr = Instantiate(trail, transform.position, Quaternion.identity);
            trails.Add(curr);
            nextTrailTime = Time.time + timeBetweenTrails;
        }

        if (Time.time > endTime)
        {
            DisableWeapon();
        }
    }

    void DisableWeapon()
    {
        foreach (GameObject curr in trails)
        {
            Destroy(curr.gameObject);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().immune = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().speed = prevSpeed;
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
            Camera.main.DOShakePosition(shakeDuration, shakeStrength);
            hitSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
            Camera.main.DOShakePosition(shakeDuration, shakeStrength);
            hitSound.Play();
        }
        //else if (collision.gameObject.tag.Equals("Player"))
        //{
        //    collision.GetComponent<Player>().Kill();
        //}
    }
}
