using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem spawnEffect;
    public GameObject bloodSplats;
    public int nbOfSplats;
    public int splatSpeedMin;
    public int splatSpeedMax;

    // Start is called before the first frame update
    void Start()
    {
        spawnEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().Kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().Kill();
        }
    }

    public void Kill()
    {
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().score++;
        for (int i = 0; i < nbOfSplats; i++)
        {
            GameObject splat = Instantiate(bloodSplats, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            splat.GetComponent<Rigidbody2D>().velocity = Random.Range(splatSpeedMin, splatSpeedMax) * splat.transform.right;
        }
        Destroy(this.gameObject);
    }
}
