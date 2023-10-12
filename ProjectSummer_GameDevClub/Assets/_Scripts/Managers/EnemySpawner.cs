using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefs = new List<GameObject>();
    [SerializeField] List<Transform> spawnPos = new List<Transform>();
    [SerializeField] float spawnTimmerWolf = 5f;
    [SerializeField] float spawnTimmerFlyingSlime = 8f;

    float spawnCounterWolf = 0;
    float spawnCounterFlyingSlime = 0;

    private void Update()
    {
        //if (spawnCounterWolf > spawnTimmerWolf)
        //{
        //    Instantiate(enemyPrefs[0], spawnPos[0].position, Quaternion.identity);
        //    spawnCounterWolf = 0;
        //}
        if (spawnCounterFlyingSlime > spawnTimmerFlyingSlime)
        {
            Instantiate(enemyPrefs[1], spawnPos[1].position, Quaternion.identity);
            spawnCounterFlyingSlime = 0;
        }
        spawnCounterFlyingSlime += Time.deltaTime;
        spawnCounterWolf += Time.deltaTime;
    }
    public void SpawnWolf()
    {
        Instantiate(enemyPrefs[0], spawnPos[0].position, Quaternion.identity);
    }
}
