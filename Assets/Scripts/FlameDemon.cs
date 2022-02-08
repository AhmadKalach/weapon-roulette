using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlameDemon : BaseWeapon
{
    public AudioSource evilLaugh;
    public AudioSource fire;
    public float timeout;

    float endTime;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isEnabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        endTime = Time.time + timeout;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isEnabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        endTime = Time.time + timeout;
        evilLaugh.Play();
        fire.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > endTime)
        {
            DisableWeapon();
        }
    }

    void DisableWeapon()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isEnabled = true;
        evilLaugh.Stop();
        fire.Stop();
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }
}
