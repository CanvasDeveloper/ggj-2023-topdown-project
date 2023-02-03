using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : Singleton<FadeController>
{
    public Animator _animator;
    public GameObject terra;
    public GameObject intro;
   
    public void StarFade()
    {


        _animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        terra.SetActive(true);
        intro.SetActive(false);
        _animator.SetTrigger("FadeIn");
    }

}
