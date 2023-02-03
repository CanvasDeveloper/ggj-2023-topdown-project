using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : Singleton<FadeController>
{
    public Animator _animator;
    public GameObject terra;
    public GameObject intro;
    public GameObject FinalBackground;
    public GameObject dialogueTerra;
    public GameObject dialogueFinal;
    public GameObject dialogueFinal1;
    public GameObject[] changeSprite;
    public GameObject spawnCut;
    public bool isFinal;
    public bool isFinaltrue;
    public bool isTerra;
   
    public void StarFade()
    {


        _animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (isFinaltrue)
        {
            WaveSpawner temp = spawnCut.GetComponent<WaveSpawner>();
           foreach( GameObject go in temp.spawnedEnemies)
            {
                go.SetActive(false);
            }

            terra.SetActive(false);
            spawnCut.SetActive(false);
            FinalBackground.SetActive(true);
            dialogueFinal1.SetActive(true);
            _animator.SetTrigger("FadeIn");
            return;
        }
        if (isFinal)
        {
            foreach (GameObject go in changeSprite)
            {
                go.GetComponent<SpriteRenderer>().enabled = false;
            }
            spawnCut.SetActive(true);
            StartCoroutine(IE_Final());
        }

        if (isTerra)
        {
            isTerra = false;
            terra.SetActive(true);
            intro.SetActive(false);
            dialogueTerra.SetActive(true);
            isFinal = true;
        }
      
        _animator.SetTrigger("FadeIn");
    }

    public IEnumerator IE_Final()
    {
        yield return new WaitForSeconds(2f);
        dialogueFinal.SetActive(true);
        isFinaltrue = true;
    }

}
