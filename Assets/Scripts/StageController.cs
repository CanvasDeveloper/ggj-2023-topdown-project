using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPonts = new List<Transform>();

    [SerializeField] List<GameObject> enemyList = new List<GameObject>();

    [HorizontalLine(1, EColor.Green)]

    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float delaySpawn;

    [HorizontalLine(1, EColor.Green)]

    [SerializeField] List<int> numberEnemythisWave = new List<int>();
    private int indexWave;
     private int numberEnemythisWavecurrent;

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
        for (int i = 0; i < totalEnemies; i++)
        {
            int IDspawn= UnityEngine.Random.Range(0, spawnPonts.Count);
            ObjectPooler.Instance.SpawnFromPool("EnemyPerto", spawnPonts[IDspawn].position, Quaternion.identity);
        }
        currentWave++;
        if(currentWave < waveCount)
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(IE_StartWave());
        }
    }

    private IEnumerator IE_Began()
    {
        yield return new WaitForSeconds(1f);
        if(numberEnemythisWavecurrent >= numberEnemythisWave[indexWave])
        {
            StopCoroutine(IE_Began());
           
        }
        else
        {
            numberEnemythisWavecurrent++;

            int temp = UnityEngine.Random.Range(0, 9);
            if (enemyList.Count > 0)
            {
                enemyList[0].transform.position = spawnPonts[temp].position;
                enemyList[0].SetActive(true);
                enemyList.Remove(enemyList[0]);
            }
            else
            {
                Instantiate(prefabEnemy, spawnPonts[temp].position, spawnPonts[temp].rotation);
            }
            yield return new WaitForSeconds(delaySpawn);
            StartCoroutine(IE_Began());
        }
       

    }
}
