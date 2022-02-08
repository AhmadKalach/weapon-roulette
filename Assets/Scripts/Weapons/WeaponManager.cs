using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public float timeBetweenWeaponsRoulette = 0.2f;
    public List<BaseWeapon> weapons;
    public BaseWeapon currWeapon;
    public float rouletteTime;
    public TextMeshProUGUI weaponNameText;
    public List<BaseWeapon> unusedWeapons;

    bool rouletteOn;
    int rouletteCounter;
    float nextRouletteImageTime;

    // Start is called before the first frame update
    void Start()
    {
        unusedWeapons = new List<BaseWeapon>();
        StartRoulette();
        nextRouletteImageTime = Time.time + timeBetweenWeaponsRoulette;
    }

    // Update is called once per frame
    void Update()
    {
        if (rouletteOn)
        {
            if (Time.time > nextRouletteImageTime)
            {
                weapons[rouletteCounter].weaponImage.SetActive(true);
                StartCoroutine(DisableImageAfterTime(weapons[rouletteCounter]));
                rouletteCounter++;
                if (rouletteCounter >= weapons.Count)
                {
                    rouletteCounter = 0;
                }
                nextRouletteImageTime = Time.time + timeBetweenWeaponsRoulette;
            }
        }
        else
        {
            foreach (BaseWeapon weapon in weapons)
            {
                weapon.weaponImage.SetActive(false);
            }
            currWeapon.weaponImage.SetActive(true);
            rouletteCounter = 0;
        }
    }

    IEnumerator DisableImageAfterTime(BaseWeapon baseWeapon)
    {
        yield return new WaitForSeconds(timeBetweenWeaponsRoulette);
        baseWeapon.weaponImage.SetActive(false);
    }

    public void StartRoulette()
    {
        if (unusedWeapons.Count == 0)
        {
            FillListIntoList(weapons, unusedWeapons);
        }
        StartCoroutine(AnimationAndWait(rouletteTime));
    }

    IEnumerator AnimationAndWait(float waitTime)
    {
        rouletteOn = true;
        foreach (BaseWeapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
            weapon.weaponImage.SetActive(false);
        }

        yield return new WaitForSeconds(waitTime);

        rouletteOn = false;
        int randIndex = Random.Range(0, unusedWeapons.Count);
        currWeapon = unusedWeapons[randIndex];
        currWeapon.gameObject.SetActive(true);
        currWeapon.weaponImage.SetActive(true);
        weaponNameText.text = currWeapon.name;
        unusedWeapons.Remove(unusedWeapons[randIndex]);
    }

    void FillListIntoList(List<BaseWeapon> from, List<BaseWeapon> to)
    {
        foreach (BaseWeapon obj in from)
        {
            to.Add(obj);
        }
    }
}
