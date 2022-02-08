using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RythmGameScript : BaseWeapon
{
    public Transform arrowStartPos;
    public float timeBetweenArrows;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    public float timeout;
    public GameObject lightningBall;
    public float expandAndDeflateTime;
    public float lightningTargetScale;
    public AudioSource audioSource;

    float nextArrowTime;
    float endTime;
    float currArrowArrivalTime;
    int currArrowId;

    // Start is called before the first frame update
    void Start()
    {
        nextArrowTime = Time.time + timeBetweenArrows;
    }

    private void OnEnable()
    {
        nextArrowTime = Time.time + timeBetweenArrows;
        endTime = Time.time + timeout;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isEnabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextArrowTime)
        {
            CreateArrowAndAnimate();
            nextArrowTime = Time.time + timeBetweenArrows;
        }

        if (Time.time > endTime)
        {
            DisableWeapon();
        }
    }

    void DisableWeapon()
    {
        WeaponManager weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        weaponManager.StartRoulette();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isEnabled = true;
    }

    public void expandLightning()
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        Sequence sequence = DOTween.Sequence();

        sequence.Append(lightningBall.transform.DOScale(new Vector3(lightningTargetScale, lightningTargetScale, 1), expandAndDeflateTime));
        sequence.Append(lightningBall.transform.DOScale(new Vector3(lightningTargetScale, lightningTargetScale, 1), expandAndDeflateTime));
        sequence.Append(lightningBall.transform.DOScale(new Vector3(0, 0, 1), expandAndDeflateTime));
        lightningBall.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        audioSource.Play();
        sequence.Play();
    }

    void CreateArrowAndAnimate()
    {
        currArrowId = Random.Range(0, 4);
        GameObject arrow = null;

        if (currArrowId == 0)
        {
            arrow = Instantiate(leftArrow, arrowStartPos.position, leftArrow.transform.rotation);
        }
        else if (currArrowId == 1)
        {
            arrow = Instantiate(rightArrow, arrowStartPos.position, rightArrow.transform.rotation);
        }
        else if (currArrowId == 2)
        {
            arrow = Instantiate(upArrow, arrowStartPos.position, upArrow.transform.rotation);
        }
        else if (currArrowId == 3)
        {
            arrow = Instantiate(downArrow, arrowStartPos.position, downArrow.transform.rotation);

        }
        arrow.GetComponent<Arrow>().arrowType = currArrowId;
        arrow.transform.parent = this.transform;

        arrow.transform.DOLocalMoveX(1.3f, 0.7f).SetEase(Ease.Linear);
    }
}
