using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSetter : BaseWeapon
{
    public GameObject mine;
    public float timeout = 5;
    public float mineRateInSeconds;

    bool disabled;
    float endTime;
    float lastBulletTime;
    float enableTime = 0.05f;
    float waitBeforeFirstShot = 0.1f;

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
        lastBulletTime = 0;
        enableTime = Time.time;
        disabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //If the "Fire1" has been released, cancel any scheduled Shoot() method executions  
        if (Time.time > endTime && !disabled)
        {
            DisableWeapon();
            disabled = true;
        }


        //If the "Fire1" button has been pressed  
        if (Time.time > lastBulletTime + mineRateInSeconds && Time.time > enableTime + waitBeforeFirstShot)
        {
            lastBulletTime = Time.time;
            SetMine();
        }
    }

    void SetMine()
    {
        GameObject.Instantiate(mine, transform.position, Quaternion.identity);
    }

    void DisableWeapon()
    {
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
    }
}
