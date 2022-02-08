using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MachineGun : BaseWeapon
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;
    public float bulletSpeed = 0f;
    public float shootRateInSeconds;
    public float bulletSpread = 0f;
    public float audioLoopTime = 0.35f;
    public float timeout = 5;
    public float knockback = 0.35f;

    bool disabled;
    float endTime;
    float lastBulletTime;
    float enableTime = 0.05f;
    float waitBeforeFirstShot = 0.1f;
    float nextAudioLoop;
    AudioSource audioSource;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        endTime = Time.time + timeout;
        enableTime = Time.time;
        disabled = false;
    }

    private void OnEnable()
    {
        endTime = Time.time + timeout;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        lastBulletTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().isEnabled = false;
        enableTime = Time.time;
        nextAudioLoop = Time.time + audioLoopTime;
        disabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAudioLoop)
        {
            audioSource.Play();
            nextAudioLoop = Time.time + audioLoopTime;
        }


        //If the "Fire1" has been released, cancel any scheduled Shoot() method executions  
        if (Time.time > endTime && !disabled)
        {
            disabled = true;
            audioSource.Stop();
            DisableWeapon();
        }


        //If the "Fire1" button has been pressed  
        if (Time.time > lastBulletTime + shootRateInSeconds && Time.time > enableTime + waitBeforeFirstShot)
        {
            lastBulletTime = Time.time;
            ShootBullet();
        }

    }

    void ShootBullet()
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        //CreateMuzzleFlash();
        Vector3 spawnPoint = bulletSpawnPoint.transform.position;
        spawnPoint.z = 1;
        GameObject instantiatedBullet = Instantiate(bullet.gameObject, spawnPoint, this.transform.rotation);
        instantiatedBullet.transform.Rotate(0, 0, Random.Range(-bulletSpread, bulletSpread));
        instantiatedBullet.GetComponent<Rigidbody2D>().velocity = instantiatedBullet.transform.right * bulletSpeed;
        player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity - (Vector2)(instantiatedBullet.transform.right * knockback);
    }

    void DisableWeapon()
    {
        player.GetComponent<Movement>().isEnabled = true;
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }
}
