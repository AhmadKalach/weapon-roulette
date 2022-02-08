using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyWarning;
    public float warningTime;
    public GameObject enemy;
    public float timeBetweenEnemies;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public List<int> enemySpawnIntervals;
    public List<float> enemySpawnSpeed;
    public float currSpawnSpeed;

    float nextEnemyTime;
    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        currSpawnSpeed = enemySpawnSpeed[0];
        nextEnemyTime = Time.time + currSpawnSpeed;
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (Time.time > nextEnemyTime)
            {
                nextEnemyTime = Time.time + currSpawnSpeed;
                StartCoroutine(spawnEnemy(warningTime));
            }

            for (int i = 0; i < enemySpawnIntervals.Count; i++)
            {
                if (scoreManager.score >= enemySpawnIntervals[i])
                {
                    currSpawnSpeed = enemySpawnSpeed[i + 1];
                }
            }
        }
    }

    IEnumerator spawnEnemy(float warningTime)
    {
        Vector2 enemyPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject warning = Instantiate(enemyWarning, enemyPos, Quaternion.identity);
        yield return new WaitForSeconds(warningTime);
        Destroy(warning.gameObject);
        Instantiate(enemy, enemyPos, Quaternion.identity);
    }
}
