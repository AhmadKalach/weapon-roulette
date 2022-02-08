using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LighSaber : BaseWeapon
{
    public float rotationsPerSecond;
    public float timeout;
    public AudioSource startSound;
    public AudioSource hitSound;

    float endTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Start is called before the first frame update
    void OnEnable()
    {
        endTime = Time.time + timeout;
        startSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - (new Vector3(0, 0, 360 * rotationsPerSecond * Time.deltaTime)));

        if (Time.time > endTime)
        {
            DisableWeapon();
        }
    }

    void DisableWeapon()
    {
        startSound.Stop();
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
            Camera.main.DOShakePosition(shakeDuration, shakeStrength);
            hitSound.Play();
        }
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
}
