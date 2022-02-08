using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOverTime : MonoBehaviour
{
    public float timeToFade;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(0, timeToFade);
        Invoke("OnFade", timeToFade);
    }

    // Update is called once per frame
    void OnFade()
    {
        Destroy(this.gameObject);
    }
}
