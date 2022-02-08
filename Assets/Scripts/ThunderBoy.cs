using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThunderBoy : BaseWeapon
{
    public AudioSource dashSound;
    public ParticleSystem particleSystem;
    public GameObject lightningBolt;
    public float timeBetweenDashes;
    public float dashTime;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float timeout;

    float nextDashTime;
    float endTime;
    bool dashed;

    // Start is called before the first frame update
    void Start()
    {
        nextDashTime = Time.time + timeBetweenDashes;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isEnabled = false;
        endTime = Time.time + timeout;
        dashed = false;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        nextDashTime = Time.time + timeBetweenDashes;
        GameObject gameObject1 = GameObject.FindGameObjectWithTag("Player");
        gameObject1.GetComponent<Movement>().isEnabled = false;
        gameObject1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject1.GetComponent<Animator>().SetBool("isRunning", false);
        endTime = Time.time + timeout;
        dashed = false;
        particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextDashTime && !dashed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clampedVector = new Vector2(Mathf.Clamp(mousePos.x, minX, maxX), Mathf.Clamp(mousePos.y, minY, maxY));
            Dash(clampedVector);
        }

        if (Time.time > endTime)
        {
            DisableWeapon();
        }
    }

    void DisableWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().isEnabled = true;
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        player.GetComponent<Player>().immune = false;
        weaponManager.StartRoulette();
    }

    void Dash(Vector2 target)
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        dashSound.Play();
        particleSystem.Stop();
        dashed = true;
        lightningBolt.SetActive(true);
        lightningBolt.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Sequence sequence = DOTween.Sequence();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().immune = true;
        float magnitude = ((Vector2)transform.position - target).magnitude;
        
        sequence.Append(player.transform.DOMove(target, dashTime)).SetEase(Ease.Linear);
        sequence.OnComplete(() => OnDashComplete(player));
        sequence.Play();
    }

    void OnDashComplete(GameObject player)
    {
        lightningBolt.SetActive(false);
        nextDashTime = Time.time + timeBetweenDashes;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Player>().immune = false;
        dashed = false;
        particleSystem.Play();
    }
}
