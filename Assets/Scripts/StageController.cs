using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] List<Transform> spawnPointsTiro = new List<Transform>();

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
    public List<ConfigEnemyWave> configEnemyWaves;

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
        else
            GameManager.Instance.GameWin();
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
                IDspawn = UnityEngine.Random.Range(0, spawnPoints.Count);
            } while
            (IDspawn == lastIDspawn);
            lastIDspawn = IDspawn;

            List<string> tagsEnemy = new List<string>();
            foreach (var w in configEnemyWaves)
            {
                if (currentWave >= w.setEnemyOnWave)
                    tagsEnemy.Add(w.tagEnemy);
            }
            int id = UnityEngine.Random.Range(0, tagsEnemy.Count);

            GameObject objEnemy = null;

            if (tagsEnemy[id].Contains("Tiro") == false)
                objEnemy = ObjectPooler.Instance.SpawnFromPool(tagsEnemy[id], spawnPoints[IDspawn].position, Quaternion.identity);
            else
                objEnemy = ObjectPooler.Instance.SpawnFromPool(tagsEnemy[id], spawnPointsTiro[IDspawn].position, Quaternion.identity);

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
        instance.IsDie = false;
        if (remainingEnemies <= 0)
            StartNewWave();
    }
}

[System.Serializable]
public class ConfigEnemyWave
{
    public string tagEnemy;
    public int setEnemyOnWave;
}