using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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

    private void OnEnable()
    {
        StartCoroutine("IE_Began");
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

            int temp = Random.Range(0, 9);
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
