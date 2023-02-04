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

    private int remainingEnemies;

    private void OnEnable()
    {
        //StartCoroutine("IE_Began");
        //StartCoroutine(IE_StartWave());
        StartNewWave();
    }

    /// <summary>
    /// Check the max wave, add +1 to current wave, start a new wave.
    /// </summary>
    /// 
    [ContextMenu("Start wave")]
    private void StartNewWave()
    {
        currentWave++;

        if (currentWave <= waveCount)
            StartCoroutine(IE_WaitTimeForStartWave());
    }

    private IEnumerator IE_WaitTimeForStartWave()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(IE_Wave());
    }

    private IEnumerator IE_Wave()
    {
        yield return new WaitForSeconds(1);
        int totalEnemies = (int)(initialEnemies * (1 + currentWave * progressionRate)); //(int)(initialEnemies * (1 + Math.Log(currentWave + 1) * progressionRate));
        remainingEnemies = 0;
        int lastIDspawn = -1;

        Debug.Log($"Total Enemies: {totalEnemies}");

        for (int i = 0; i < totalEnemies; i++)
        {
            //get a random spawnpoint
            int IDspawn;
            do
            {
                IDspawn = UnityEngine.Random.Range(0, spawnPonts.Count);
            } while
            (IDspawn == lastIDspawn);
            lastIDspawn = IDspawn;

            var objEnemy = ObjectPooler.Instance.SpawnFromPool("EnemyPerto", spawnPonts[IDspawn].position, Quaternion.identity);
            var enemy = objEnemy.GetComponent<IDamageable>();

            remainingEnemies++;

            enemy.OnDie += OnEnemyDie;

            yield return new WaitForSeconds(delaySpawn / (currentWave + 1));
        }
    }

    private void OnEnemyDie(IDamageable instance)
    {
        instance.OnDie -= OnEnemyDie;
        enemyList.Remove(instance);

        remainingEnemies--;
        Debug.Log(remainingEnemies);

        if (remainingEnemies <= 0)
            StartNewWave();
    }
}
