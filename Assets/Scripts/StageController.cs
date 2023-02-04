using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPonts = new List<Transform>();

    [SerializeField] Dictionary<IDamageable, GameObject> enemyList = new Dictionary<IDamageable, GameObject>();

    [HorizontalLine(1, EColor.Green)]

    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float delaySpawn;

    [HorizontalLine(1, EColor.Green)]
    [Header("Config Wave")]
    public int waveCount = 10;
    public int initialEnemies = 10;
    public float progressionRate = 0.1f;

    [SerializeField] private int currentWave = 0;

    private void OnEnable()
    {
        //StartCoroutine("IE_Began");
        StartCoroutine(IE_StartWave());
    }

    private IEnumerator IE_StartWave()
    {
        yield return new WaitForSeconds(1);
        int totalEnemies = (int)(initialEnemies * (1 + Math.Log(currentWave + 1) * progressionRate));
        Debug.Log($"Total Enemies: {totalEnemies}");

        int lastIDspawn = -1;
        for (int i = 0; i < totalEnemies; i++)
        {
            int IDspawn;
            do
            {
                IDspawn = UnityEngine.Random.Range(0, spawnPonts.Count);
            } while (IDspawn == lastIDspawn);
            lastIDspawn = IDspawn;

            var objEnemy = ObjectPooler.Instance.SpawnFromPool("EnemyPerto", spawnPonts[IDspawn].position, Quaternion.identity);
            var enemy = objEnemy.GetComponent<IDamageable>();
            enemy.OnDie += () => 
            {
                enemyList.Remove(enemy);     
            };
            yield return new WaitForSeconds(delaySpawn / (currentWave + 1));
        }

        int remainingEnemies = 0;
        foreach (var enemy in enemyList.Values)
        {
            if (enemy != null)
            {
                remainingEnemies++;
            }
        }

        if (remainingEnemies == 0)
        {
            currentWave++;
            if (currentWave < waveCount)
            {
                yield return new WaitForSeconds(5);
                StartCoroutine(IE_StartWave());
            }
        }
        else
        {
            StartCoroutine(IE_StartWave());
        }
    }

}
